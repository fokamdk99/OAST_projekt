using System.Collections.Generic;
using OAST.Events;

namespace OAST.Queue
{
    public interface ICustomQueue
    {
        List<Event> Queue { get; set; }
        int QueueSize { get; set; }

        void Put(Event @event);
        Event Get();
        void Reset();
        void SetQueueSize(int queueSize);
        void IncrementNumberOfProcessedEvents();
        int GetNumberOfProcessedEvents();
    }
}