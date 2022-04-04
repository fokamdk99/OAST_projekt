using System.Collections.Generic;
using OAST.Events;
using OAST.OASTPackages;

namespace OAST.Tools.Generators
{
    public interface IEventGenerator
    {
        int NumberOfEvents { get; set; }
        int NumberOfCreatedEvents { get; set; }
        double LastGeneratedTime { get; set; }
        List<Event> InitializeEventsList(int numberOfEvents, SourceType sourceType, int seed, int lambda);
        List<Event> CreateEvents(SourceType sourceType, int seed, int lambda);
        Event CreateFinishEvent(Package package, double currentTime, double processingTime);
    }
}