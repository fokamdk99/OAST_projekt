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

        public Simulator(ICustomQueue customQueue,
            ICustomServer customServer,
            IEventHandler eventHandler)
        {
            _customQueue = customQueue;
            _customServer = customServer;
            _eventHandler = eventHandler;
        }

        public void Run(int queueSize, int numberOfRepetitions, int lambda, int mi)
        {
            _customQueue.SetQueueSize(queueSize);
            _customServer.SetMi(mi);

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

                    _eventHandler.HandleEvent(_customQueue.EventsList[i], i);

                    i += 1;
                }

                _customQueue.Sort();

                Statistic.SimulationTime = _customQueue.EventsList[_customQueue.EventsList.Count - 1].Time;

                PrintStatistics();
            }

            Statistic.Calculate();
            Logs.SaveStatistic();
        }

        public void InitializeStatistics()
        {
            Parameters.CalculateTimeBetweenPackages();
        }
        public void PrintStatistics()
        {
            Parameters.PrintMainParameters();
            Statistic.PrintStatistics();
            Statistic.PrintAverageTimeInQueue();
            Statistic.PrintServerLoad();

            Logs.SaveEventList(_customQueue.EventsList);

            Statistic.GlobalList.Add(new GlobalStatistic());
            Statistic.ResetStatistics();
        }
    }
}