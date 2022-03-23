using System.Collections.Generic;
using MOPS.Events;
using MOPS.Queue;
using MOPS.Server;
using MOPS.Tools;
using MOPS.Tools.Generators;

namespace MOPS.Simulator
{
    public class Simulator : ISimulator
    {
        private readonly ICustomQueue _customQueue;
        private readonly ICustomServer _customServer;
        private readonly IEventHandler _eventHandler;

        public Simulator(ICustomQueue customQueue,
            ICustomServer customServer,
            IEventHandler eventHandler)
        {
            _customQueue = customQueue;
            _customServer = customServer;
            _eventHandler = eventHandler;
        }

        public void Run(int queueSize, int serverBitRate, int numberOfRepetitions, int lambda)
        {
            _customQueue.SetQueueSize(queueSize);

            _customServer.BitRate = serverBitRate;

            InitializeStatistics();
            
            for (int n = 0; n < numberOfRepetitions; n++)
            {
                _customQueue.Reset();
                _customQueue.InitializeEventsList(Parameters.numberOfPackages, SourceType.Poisson, n+1000, lambda);
                
                _customServer.Reset();

                double deltaTime = 0;
                bool flag = false;

                int i = 0;
                while (_customQueue.GetNumberOfProcessedEvents() != _customQueue.EventsList.Count)
                {
                    Statistic.Time = _customQueue.EventsList[i].Time;

                    if (flag == true)
                    {
                        deltaTime = _customQueue.EventsList[i].Time - _customQueue.EventsList[i - 1].Time;
                        Statistic.AddAveragePackageInQueue(_customQueue.Queue.Count, deltaTime);
                    }

                    flag = true;

                    _eventHandler.HandleEvent(_customQueue.EventsList[i], i);
                    _customQueue.IncrementNumberOfProcessedEvents();
                    
                    i += 1;
                    
                }

                _customQueue.Sort();

                Statistic.SimulationTime = _customQueue.EventsList[_customQueue.EventsList.Count - 1].Time;

                PrintStatistics();

                _customQueue.EventsList = new List<Event>();
            }

            Statistic.Calculate();
            Logs.SaveStatistic();
        }

        public void InitializeStatistics()
        {
            Parameters.CalculateServerTime();
            Parameters.CalculateTimeBetweenPackages();
            Parameters.PrintAllParameters();
            Logs.SaveServerParameters();
        }
        public void PrintStatistics()
        {
            Parameters.PrintMainParameters();
            Statistic.PrintStatistics();
            Statistic.PrintAveragePackageInQueue();
            Statistic.PrintAverageTimeInQueue();
            Statistic.PrintServerLoad();

            Logs.SaveEventList(_customQueue.EventsList);

            Statistic.GlobalList.Add(new GlobalStatistic());
            Statistic.ResetStatistics();
        }
    }
}