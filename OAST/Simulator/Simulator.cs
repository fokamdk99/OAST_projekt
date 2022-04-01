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
                _customServer.Reset();
                _queueMeasurements.Reset();
                _serverMeasurements.Reset();
                _customQueue.Reset();
                
                _customQueue.InitializeEventsList(Parameters.numberOfPackages, SourceType.Poisson, n+1000, lambda);

                int i = 0;
                while (_customQueue.GetNumberOfProcessedEvents() < _customQueue.EventsList.Count)
                {
                    _statistic.Time = _customQueue.EventsList[i].Time;

                    _eventHandler.HandleEvent(_customQueue.EventsList[i], i);

                    i += 1;

                    if (_eventGenerator.NumberOfCreatedEvents < _eventGenerator.NumberOfEvents)
                    {
                        _customQueue.EventsList.
                            AddRange(_eventGenerator.CreateEvents(SourceType.Poisson, i+1000, lambda));
                        
                        _customQueue.Sort();
                    }
                }

                _customQueue.Sort();

                _statistic.SimulationTime = _customQueue.EventsList[_customQueue.EventsList.Count - 1].Time;

                PrintStatistics();
            }
            
            Calculate();
            _customQueue.ShowQueueMeasurements();
            _logs.SaveStatistic();
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
                _queueMeasurements.NumberOfPackagesInQueue,
                _queueMeasurements.AverageTimeinQueue,
                _queueMeasurements.AverageNumberOfPackagesInQueue,
                _statistic.SimulationTime,
                _serverMeasurements.ServerLoad));
            
            _statistic.ResetStatistics();
        }
        
        public void Calculate()
        {
            var globalMeasurements = new AggregateMeasurements();
            
            foreach (var e in _aggregateMeasurements)
            {
                globalMeasurements.NumberOfReceivedPackages += e.NumberOfReceivedPackages;
                globalMeasurements.NumberOfLostPackages += e.NumberOfLostPackages;
                globalMeasurements.NumberOfPackagesInQueue += e.NumberOfPackagesInQueue;
                globalMeasurements.AverageTimeInQueue += e.AverageTimeInQueue;
                globalMeasurements.AverageNumberOfPackagesInQueue += e.AverageNumberOfPackagesInQueue;
                globalMeasurements.SimulationTime += e.SimulationTime;
                globalMeasurements.ServerLoad += e.ServerLoad;
            }

            globalMeasurements.NumberOfPackagesInQueue = globalMeasurements.NumberOfPackagesInQueue / 100;
            globalMeasurements.AverageTimeInQueue = globalMeasurements.AverageTimeInQueue / 100;
            globalMeasurements.AverageNumberOfPackagesInQueue = globalMeasurements.AverageNumberOfPackagesInQueue / 100;
            globalMeasurements.ServerLoad = globalMeasurements.ServerLoad / 100;
            globalMeasurements.PercentOfSuccess = ( globalMeasurements.NumberOfReceivedPackages - globalMeasurements.NumberOfLostPackages) /
                globalMeasurements.NumberOfReceivedPackages * 100;
        }
    }
}