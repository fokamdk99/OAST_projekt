using System.Collections.Generic;
using OAST.Events;
using OAST.OASTPackages;
using OAST.Tools.Generators;

namespace OAST.Queue
{
    public interface ICustomQueue
    {
        List<Event> EventsList { get; set; }
        List<Package> Queue { get; set; }
        int SortingIndicator { get; set; }
        int MaxSortLength { get; set; }
        int MaxQueueLength { get; set; }

        void Put(Package package);
        Package Get();
        void RemovePackageFromQueue();
        void Sort();
        void Reset();
        void InitializeEventsList(int numberOfEvents, SourceType eventType, int seed, int lambda);
        void SetQueueSize(int queueSize);
        void IncrementNumberOfProcessedEvents();
        int GetNumberOfProcessedEvents();
        void ShowQueueMeasurements();
    }
}