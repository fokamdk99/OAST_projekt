using System.Collections.Generic;
using System.Linq;
using OAST.Events;

namespace OAST.Queue
{
    public class CustomQueue : ICustomQueue
    {
        public List<Event> Queue { get; set; }
        public int QueueSize { get; set; }
        private int NumberOfProcessedEvents { get; set; }

        public CustomQueue(int queueSize)
        {
            QueueSize = queueSize;
            Queue = new List<Event>();
        }
        
        public void Put(Event @event)
        {
            Queue.Add(@event);
            Sort();
        }

        public Event Get()
        {
            var result = Queue.ElementAt(0);
            Queue.RemoveAt(0);
            return result;
        }

        private void Sort()
        {
            Queue.Sort(new EventsComparer());
        }

        public void Reset()
        {
            Queue = new List<Event>();
            NumberOfProcessedEvents = 0;
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