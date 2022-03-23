using System.Collections.Generic;
using MOPS.Events;
using MOPS.Packages;

namespace MOPS.Tools.Generators
{
    public interface IEventGenerator
    {
        List<Event> InitializeEventsList(int numberOfEvents, SourceType sourceType, int seed, int lambda);
        Event CreateFinishEvent(Package package, double currentTime, double processingTime);
    }
}