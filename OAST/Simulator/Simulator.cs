using OAST.Events;
using OAST.Queue;
using OAST.Server;
using OAST.Tools;

namespace OAST.Simulator
{
    public class Simulator : ISimulator
    {
        private readonly ICustomQueue _customQueue;
        private readonly ICustomServer _customServer;
        private readonly IEventHandler _eventHandler;
        private readonly IServerMeasurements _serverMeasurements;
        private readonly IQueueMeasurements _queueMeasurements;
        private readonly IParameters _parameters;
        private readonly IStatisticAggregator _statisticAggregator;


        public Simulator(ICustomQueue customQueue,
            ICustomServer customServer,
            IEventHandler eventHandler, 
            IServerMeasurements serverMeasurements, 
            IQueueMeasurements queueMeasurements,
            IParameters parameters, 
            IStatisticAggregator statisticAggregator)
        {
            _customQueue = customQueue;
            _customServer = customServer;
            _eventHandler = eventHandler;
            _serverMeasurements = serverMeasurements;
            _queueMeasurements = queueMeasurements;
            _parameters = parameters;
            _statisticAggregator = statisticAggregator;
        }

        public void Run()
        {
            _customQueue.SetQueueSize(_parameters.QueueSize);
            _customServer.SetMi(_parameters.Mi);

            for (int n = 0; n < _parameters.NumberOfSimulations; n++)
            {
                var statistic = Simulate();
                _statisticAggregator.AddStatistic(statistic);
            }
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
            _queueMeasurements.Reset();
            _serverMeasurements.Reset();
            _customQueue.Reset();
        }
    }
}