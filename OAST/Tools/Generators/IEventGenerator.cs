using System.Collections.Generic;
using OAST.Events;
using OAST.Packages;

namespace OAST.Tools.Generators
{
    public interface IEventGenerator
    {
        List<Event> InitializeEventsList(int numberOfEvents, SourceType sourceType, int seed, int lambda);
        Event CreateFinishEvent(Package package, double currentTime, double processingTime);
    }
}