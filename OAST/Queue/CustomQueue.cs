using System.Collections.Generic;
using System.Linq;
using OAST.Events;
using OAST.Packages;
using OAST.Tools.Generators;

namespace OAST.Queue
{
    public class CustomQueue : ICustomQueue
    {
        private readonly IEventGenerator _eventGenerator;
        
        public List<Event> EventsList { get; set; }
        public List<Package> Queue { get; set; }
        private int QueueSize { get; set; }
        private int NumberOfProcessedEvents { get; set; }

        public CustomQueue(IEventGenerator eventGenerator)
        {
            _eventGenerator = eventGenerator;
            EventsList = new List<Event>();
            Queue = new List<Package>();
        }

        public CustomQueue(int queueSize, IEventGenerator eventGenerator) : base()
        {
            QueueSize = queueSize;
            _eventGenerator = eventGenerator;
        }
        
        public void Put(Package package)
        {
            Queue.Add(package);
        }

        public Package Get()
        {
            return Queue.ElementAt(0);
        }

        public void Sort()
        {
            EventsList.Sort((x, y) => x.Time.CompareTo(y.Time));
        }

        public void Reset()
        {
            Queue = new List<Package>();
            EventsList = new List<Event>();
            NumberOfProcessedEvents = 0;
        }

        public void InitializeEventsList(int numberOfEvents, SourceType eventType, int seed, int lambda)
        {
            EventsList = _eventGenerator.InitializeEventsList(numberOfEvents, eventType, seed, lambda);
            Sort();
        }

        public void SetQueueSize(int queueSize)
        {
            QueueSize = queueSize;
        }

        public void IncrementNumberOfProcessedEvents()
        {
            NumberOfProcessedEvents += 1;
        }

        public int GetNumberOfProcessedEvents()
        {
            return NumberOfProcessedEvents;
        }
    }
}