using System.Linq;
using MOPS.Packages;
using MOPS.Queue;
using MOPS.Server;
using MOPS.Tools;
using MOPS.Tools.Generators;

namespace MOPS.Events
{
    public class EventHandler : IEventHandler
    {
        private readonly ICustomQueue _customQueue;
        private readonly ICustomServer _customServer;
        private readonly IEventGenerator _eventGenerator;

        public EventHandler(ICustomQueue customQueue, 
            ICustomServer customServer, 
            IEventGenerator eventGenerator)
        {
            _customQueue = customQueue;
            _customServer = customServer;
            _eventGenerator = eventGenerator;
        }

        public void HandleEvent(Event @event, int eventId)
        {
            if (@event.Type == EventType.Coming)
            {
                HandleComingEvent(@event, eventId);
            }
            else
            {
                HandleFinishEvent();
            }
        }
        
        public void HandleComingEvent(Event @event, int eventId)
        {
            Package package;
            
            package = @event.CreatePackage(eventId);
            Statistic.IncrementNumberOfReceivedPackages();

            if (_customServer.Busy) // serwer zajety
            {
                if (_customQueue.Queue.Count() < Parameters.queueSize) // jest miejsce w kolejce
                {
                    package.AddToQueueTime = Statistic.Time;
                    _customQueue.Put(package);
                    Statistic.IncrementNumberOfPackagesInQueue();
                }
                else // nie ma miejsca w kolejce
                {
                    Statistic.IncrementNumberOfLostPackages();
                }
            }
            else // serwer wolny
            {
                if (_customQueue.Queue.Count == 0) // kolejka pusta
                {
                    _customServer.SetBusy();
                    _customQueue.EventsList.Add(
                        _eventGenerator.CreateFinishEvent(package, Statistic.Time, Parameters.serverTime));
                    _customQueue.Sort();
                }
                else // Cos jest w kolejce
                {
                    _customQueue.Put(package);
                    _customServer.SetBusy();
                    _customQueue.EventsList.Add(_eventGenerator.CreateFinishEvent(_customQueue.Queue[0],
                        Statistic.Time, Parameters.serverTime));
                    _customQueue.Sort();
                    _customQueue.Queue[0].GetFromQueueTime = Statistic.Time;
                    Statistic.AddAverageTimeinQueue(_customQueue.Queue[0].GetFromQueueTime -
                                                    _customQueue.Queue[0].AddToQueueTime);
                    _customQueue.Queue.RemoveAt(0);
                }
            }
        }
        
        public void HandleFinishEvent()
        {
            if (_customQueue.Queue.Count != 0)
            {
                _customQueue.EventsList.Add(_eventGenerator.CreateFinishEvent(_customQueue.Queue[0], Statistic.Time,
                    Parameters.serverTime));
                _customQueue.Sort();
                _customQueue.Queue[0].GetFromQueueTime = Statistic.Time;
                Statistic.AddAverageTimeinQueue(_customQueue.Queue[0].GetFromQueueTime -
                                                _customQueue.Queue[0].AddToQueueTime);
                _customQueue.Queue.RemoveAt(0);
            }
            else
            {
                _customServer.SetAvailable();
            }
        }
    }
}