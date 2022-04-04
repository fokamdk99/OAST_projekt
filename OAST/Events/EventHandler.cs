using System.Linq;
using OAST.OASTPackages;
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
        private readonly IEventGenerator _eventGenerator;
        private readonly IQueueMeasurements _queueMeasurements;
        private readonly IStatistic _statistic;

        public EventHandler(ICustomQueue customQueue, 
            ICustomServer customServer, 
            IEventGenerator eventGenerator, 
            IQueueMeasurements queueMeasurements, 
            IStatistic statistic)
        {
            _customQueue = customQueue;
            _customServer = customServer;
            _eventGenerator = eventGenerator;
            _queueMeasurements = queueMeasurements;
            _statistic = statistic;
        }

        public void HandleEvent(Event @event, int eventId)
        {
            if (@event.Type == EventType.Coming)
            {
                HandleComingEvent(@event, eventId);
            }
            else
            {
                HandleFinishEvent(eventId);
            }
            
            _customQueue.IncrementNumberOfProcessedEvents();
        }
        
        public void HandleComingEvent(Event @event, int eventId)
        {
            Package package;
            
            package = @event.CreatePackage(eventId);
            _statistic.IncrementNumberOfReceivedPackages();

            if (_customServer.Busy) // serwer zajety
            {
                if (_customQueue.Queue.Count() < Parameters.queueSize) // jest miejsce w kolejce
                {
                    package.AddToQueueTime = _statistic.Time;
                    _customQueue.Put(package);
                    _queueMeasurements.IncrementNumberOfPackagesInQueue();
                }
                else // nie ma miejsca w kolejce
                {
                    _statistic.IncrementNumberOfLostPackages();
                }
            }
            else // serwer wolny
            {
                double processingTime = _customServer.GenerateProcessingTime(SourceType.Poisson, eventId + 20000);
                if (_customQueue.Queue.Count == 0) // kolejka pusta
                {
                    _customServer.SetBusy();
                    
                    _customQueue.EventsList.Add(
                        _eventGenerator.CreateFinishEvent(package, _statistic.Time, processingTime));
                    _customQueue.Sort();
                }
            }
        }
        
        public void HandleFinishEvent(int eventId)
        {
            // jesli kolejka nie jest pusta, wygeneruj czas obslugi pierwszego zdarzenia typu coming w kolejce, po czym
            // usun go z kolejki. Zauwaz, ze do kolejki wrzucasz tylko zdarzenia typu coming
            _customQueue.SortingIndicator = eventId;
            if (_customQueue.Queue.Count != 0)
            {
                double processingTime = _customServer.GenerateProcessingTime(SourceType.Poisson, eventId + 30000);
                _customQueue.EventsList.Add(_eventGenerator.CreateFinishEvent(_customQueue.Queue[0], _statistic.Time,
                    processingTime));
                _customQueue.Sort();
                _customQueue.Queue[0].GetFromQueueTime = _statistic.Time;
                _queueMeasurements.AddAverageTimeinQueue(_customQueue.Queue[0].GetFromQueueTime -
                                                _customQueue.Queue[0].AddToQueueTime);
                _customQueue.RemovePackageFromQueue();
            }
            else
            {
                _customServer.SetAvailable();
            }
        }
    }
}