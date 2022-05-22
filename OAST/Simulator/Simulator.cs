using System;
using System.Collections.Generic;
using System.Linq;
using OAST.Events;
using OAST.Queue;
using OAST.Server;
using OAST.Tools;
using OAST.Tools.Generators;

namespace OAST.Simulator
{
    public class Simulator : ISimulator
    {
        private readonly ICustomQueue _customQueue;
        private readonly ICustomServer _customServer;
        private readonly IEventHandler _eventHandler;
        private readonly IServerMeasurements _serverMeasurements;
        private readonly IQueueMeasurements _queueMeasurements;
        private readonly IStatistic _statistic;
        private readonly ILogs _logs;
        private readonly IEventGenerator _eventGenerator;
        private List<AggregateMeasurements> _aggregateMeasurements;

        public Simulator(ICustomQueue customQueue,
            ICustomServer customServer,
            IEventHandler eventHandler, 
            IServerMeasurements serverMeasurements, 
            IQueueMeasurements queueMeasurements, 
            IStatistic statistic, 
            ILogs logs, 
            IEventGenerator eventGenerator)
        {
            _customQueue = customQueue;
            _customServer = customServer;
            _eventHandler = eventHandler;
            _serverMeasurements = serverMeasurements;
            _queueMeasurements = queueMeasurements;
            _statistic = statistic;
            _logs = logs;
            _eventGenerator = eventGenerator;
            _aggregateMeasurements = new List<AggregateMeasurements>();
        }

        public void Run(int queueSize, int numberOfRepetitions, int lambda, int mi)
        {
            _customQueue.SetQueueSize(queueSize);
            _customServer.SetMi(mi);
            
            for (int n = 0; n < numberOfRepetitions; n++)
            {   
                Console.WriteLine("Simulation number:" + n);
                _customServer.Reset();
                _queueMeasurements.Reset();
                _serverMeasurements.Reset();
                _customQueue.Reset();
                
                _customQueue.InitializeEventsList(Parameters.numberOfPackages, SourceType.Poisson, n+1000, lambda);

                int startInterval = 400;
                int numberOfInterval = 10;
                int i = 0;
                while (_customQueue.GetNumberOfProcessedEvents() < _customQueue.EventsList.Count)
                {
                    _statistic.Time = _customQueue.EventsList[i].Time;

                    _eventHandler.HandleEvent(_customQueue.EventsList[i], i);
                    
                    if (i % startInterval == 0)
                    {
                        if (i >= 1200)
                        {
                            if (i != 1200)
                            {
                                PrintStatistics();
                            }

                            _statistic.NumberOfReceivedPackages = 0;
                            _statistic.NumberOfLostPackages = 0;
                            _serverMeasurements.ProcessedPackages = 0;
                            _queueMeasurements.NumberOfPackagesThatWereQueue = 0;
                            _queueMeasurements.NumberOfPackagesThatWereNotQueue = 0;
                            _queueMeasurements.TimeInQueue = 0;
                            _serverMeasurements.ProcessingTime = 0;
                        }

                    }  
                    
                    i += 1;
                    
                    // if (_eventGenerator.NumberOfCreatedEvents < _eventGenerator.NumberOfEvents)
                    // {
                    //     _customQueue.EventsList.
                    //         AddRange(_eventGenerator.CreateEvents(SourceType.Poisson, i+1000, lambda));
                    //     _customQueue.Sort();
                    // }
                }

                _customQueue.Sort();

                _statistic.SimulationTime = _customQueue.EventsList[_customQueue.EventsList.Count - 1].Time;
                
                
            }
            PrintStatistics();
            Calculate();
            _customQueue.ShowQueueMeasurements();
            _logs.SaveStatistic();
            InitialConditions();
        }
        
        public void PrintStatistics()
        {
            Parameters.PrintMainParameters();
            _statistic.PrintStatistics();
            _queueMeasurements.PrintAverageTimeInQueue();
            _serverMeasurements.PrintServerLoad(_statistic.SimulationTime);

            _logs.SaveEventList(_customQueue.EventsList);

            _aggregateMeasurements.Add(new AggregateMeasurements(
                _statistic.NumberOfReceivedPackages,
                _statistic.NumberOfLostPackages,
                _serverMeasurements.ProcessedPackages, 
                _queueMeasurements.NumberOfPackagesThatWereQueue,
                _queueMeasurements.NumberOfPackagesThatWereNotQueue,
                _queueMeasurements.TimeInQueue,
                _serverMeasurements.ProcessingTime,
                _serverMeasurements.CalculateAverageServerProcessingTime(),
                _statistic.SimulationTime));
            
            //_statistic.ResetStatistics();
        } 
        
        public void Calculate()
        {
            _statistic.ResetStatistics();
            int numberOfRepetitions = 1;
            var globalMeasurements = new AggregateMeasurements();
            
            foreach (var e in _aggregateMeasurements)
            {
                globalMeasurements.NumberOfReceivedPackages += e.NumberOfReceivedPackages;
                globalMeasurements.NumberOfLostPackages += e.NumberOfLostPackages;
                globalMeasurements.NumberOfProcessedPackages+= e.NumberOfProcessedPackages;
                globalMeasurements.NumberOfPackagesThatWereInQueue += e.NumberOfPackagesThatWereInQueue;
                globalMeasurements.NumberOfPackagesThatWereNotInQueue += e.NumberOfPackagesThatWereNotInQueue;
                globalMeasurements.AverageTimeInQueue += e.AverageTimeInQueue;
                globalMeasurements.ProcessingTime += e.ProcessingTime;
                globalMeasurements.AverageProcessingTime += e.AverageProcessingTime;
                globalMeasurements.SimulationTime += e.SimulationTime;
            }

            //VARIANCE 
            var variancesForRepetitions = globalMeasurements.AverageTimeInQueue / (numberOfRepetitions*globalMeasurements.NumberOfPackagesThatWereInQueue);
            double variance2 = 0;
            foreach (var e in _aggregateMeasurements)
            {    
                double variancesForSpecificRepetition = (e.AverageTimeInQueue/e.NumberOfPackagesThatWereInQueue) - variancesForRepetitions;
                 variance2 =+ variancesForSpecificRepetition* variancesForSpecificRepetition;
            }
            
            
            double variance = variance2 / (numberOfRepetitions - 1);
            Console.WriteLine("Variance:" + variance);
            Console.WriteLine("AverageWaitTimeInQueue: " + globalMeasurements.AverageTimeInQueue/globalMeasurements.NumberOfPackagesThatWereInQueue);
            Console.WriteLine("AverageProcessingTime: " + globalMeasurements.ProcessingTime/globalMeasurements.NumberOfProcessedPackages);
        }

        public void InitialConditions()
        {
            var finishEvents = _customQueue.EventsList.Where(x => x.Type == EventType.Finish).ToList();
            var mean = finishEvents.Count;//(finishEvents.Last().Time - finishEvents.First().Time) / finishEvents.Count;
            List<List<double>> batchesCollection = new List<List<double>>();
            List<double> variances = new List<double>();
            List<int> numberOfSegments = new List<int>
            {
                //1, 2, 4, 8, 16, 32, 64
            };
            numberOfSegments.AddRange(Enumerable.Range(1, 64));

            foreach (var segments in numberOfSegments)
            {
                batchesCollection.Add(CreateRange(finishEvents.First().Time - 0.01, finishEvents.Last().Time, segments));
            }

            foreach (var item in batchesCollection.Select((value, i) => new {i, value}))
            {
                double sum = 0;
                for (int i = 0; i < item.value.Count - 1; i++)
                {
                    var partialMean = finishEvents.
                        Count(x => x.Time > item.value.ElementAt(i) && x.Time <= item.value.ElementAt(i + 1));
                    sum += Math.Pow(partialMean - mean, 2);
                }

                var variance = sum / (numberOfSegments.ElementAt(item.i) - 1);
                variances.Add(variance);
                
            }
            //Console.WriteLine(v);
            _logs.SaveVariances(variances);
        }
        
        public List<double> CreateRange(double start, double stop, int numberOfPoints)
        {
            List<double> list = new List<double>();

            // Some edge case handle(step = 0, step = (-)Infinity, etc

            var step = (stop - start) / (numberOfPoints - 1);
            var range = Enumerable.Range(0, numberOfPoints);

            foreach (var num in range)
            {
                list.Add(start + num * step);
            }

            return list;
        }
    }
}