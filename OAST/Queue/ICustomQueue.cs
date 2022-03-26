using System.Collections.Generic;
using OAST.Events;
using OAST.Packages;
using OAST.Tools.Generators;

namespace OAST.Queue
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