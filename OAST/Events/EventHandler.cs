using System.Linq;
using OAST.Queue;
using OAST.Server;
using OAST.Tools;
using OAST.Tools.Generators;

namespace OAST.Events
{
    public class EventHandler : IEventHandler
    {
        private readonly ICustomQueue _customQueue;
        private readonly ICustomServer _customServer;
        private readonly INumberGenerator _numberGenerator;
        private readonly IParameters _parameters;

        public EventHandler(ICustomQueue customQueue, 
            ICustomServer customServer,
            INumberGenerator numberGenerator, 
            IParameters parameters)
        {
            _customQueue = customQueue;
            _customServer = customServer;
            _numberGenerator = numberGenerator;
            _parameters = parameters;
        }

        public void HandleEvent(Event @event, ref Statistic statistic)
        {
            if (@event.Type == EventType.Coming)
            {
                HandleComingEvent(@event, ref statistic);
            }
            else
            {
                HandleFinishEvent(@event, ref statistic);
            }
            
            _customQueue.IncrementNumberOfProcessedEvents();
        }
        
        public void HandleComingEvent(Event @event, ref Statistic statistic)
        {
            NewEvent(EventType.Coming, @event.Time);

            int newRequest = statistic.NewEntry();
            statistic.ArrivalTime[newRequest] = @event.Time;
            statistic.FillOnArrival[newRequest] = _customServer.Queue.Count;

            if (_customServer.Queue.Count < _customServer.QueueSize) // jest miejsce w kolejce
            {
                statistic.Blocked[newRequest] = false;
                if (_customServer.Queue.Count == 0)
                {
                    statistic.ServiceStartTime[newRequest] = @event.Time;
                    NewEvent(EventType.Finish, @event.Time);
                }
                
                _customServer.Queue.Add(newRequest);
            }
            else
            {
                statistic.Blocked[newRequest] = true;
            }
        }
        
        public void HandleFinishEvent(Event @event, ref Statistic statistic)
        {
            // jesli kolejka nie jest pusta, wygeneruj czas obslugi pierwszego zdarzenia typu coming w kolejce, po czym
            // usun go z kolejki. Zauwaz, ze do kolejki wrzucasz tylko zdarzenia typu coming

            var request = _customServer.Get();
            statistic.DepartureTime[request] = @event.Time;
            
            if (_customServer.Queue.Count > 0)
            {
                statistic.ServiceStartTime[_customServer.Queue.ElementAt(0)] = @event.Time;
                NewEvent(EventType.Finish, @event.Time);
            }
        }

        private void NewEvent(EventType eventType, double time)
        {
            Event newEvent;
            if (eventType == EventType.Coming)
            {
                newEvent = new Event(eventType, time + _numberGenerator.GetInterval(SourceType.Poisson, _parameters.Lambda));
            }
            else
            {
                newEvent = new Event(eventType, time + _numberGenerator.GetInterval(SourceType.Poisson, _parameters.Mi));
            }
            
            _customQueue.Put(newEvent);
        }
    }
}