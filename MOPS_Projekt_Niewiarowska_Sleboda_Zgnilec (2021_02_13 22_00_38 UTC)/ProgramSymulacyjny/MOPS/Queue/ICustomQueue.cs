using System.Collections.Generic;
using MOPS.Events;
using MOPS.Packages;
using MOPS.Tools.Generators;

namespace MOPS.Queue
{
    public interface ICustomQueue
    {
        List<Event> EventsList { get; set; }
        List<Package> Queue { get; set; }

        void Put(Package package);
        Package Get();
        void Sort();
        void Reset();
        void InitializeEventsList(int numberOfEvents, SourceType eventType, int seed, int lambda);
        void SetQueueSize(int queueSize);
        void IncrementNumberOfProcessedEvents();
        int GetNumberOfProcessedEvents();
    }
}