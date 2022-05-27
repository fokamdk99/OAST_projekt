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
        private readonly IParameters _parameters;
        private readonly IStatisticAggregator _statisticAggregator;
        private readonly INumberGenerator _numberGenerator;


        public Simulator(ICustomQueue customQueue,
            ICustomServer customServer,
            IEventHandler eventHandler,
            IParameters parameters, 
            IStatisticAggregator statisticAggregator, 
            INumberGenerator numberGenerator)
        {
            _customQueue = customQueue;
            _customServer = customServer;
            _eventHandler = eventHandler;
            _parameters = parameters;
            _statisticAggregator = statisticAggregator;
            _numberGenerator = numberGenerator;
        }

        public void Run()
        {
            _customQueue.SetQueueSize(_parameters.QueueSize);
            _customServer.SetMi(_parameters.Mi);
            _customServer.SetQueueSize(_parameters.QueueSize);

            for (int n = 0; n < _parameters.NumberOfSimulations; n++)
            {
                _numberGenerator.SetSeed(98647 + (n+1) * 34);
                var statistic = Simulate();
                _statisticAggregator.AddStatistic(statistic);
            }
            
            _statisticAggregator.Calculate();
        }

        public IStatistic Simulate()
        {
            var statistic = new Statistic();
            //set seed, create new queue, initial event, reset statistics
            ResetParams();

            var @event = new Event
            {
                Type = EventType.Coming,
                Time = 0
            };

            while (@event.Time < _parameters.SimulationTime)
            {
                _eventHandler.HandleEvent(@event, ref statistic);
                @event = _customQueue.Get();
            }

            return statistic;
        }

        private void ResetParams()
        {
            _customServer.Reset();
            _customQueue.Reset();
        }
    }
}