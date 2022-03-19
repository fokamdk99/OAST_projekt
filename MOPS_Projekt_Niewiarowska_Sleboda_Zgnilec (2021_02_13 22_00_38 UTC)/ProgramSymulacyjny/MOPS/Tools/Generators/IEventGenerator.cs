using System.Collections.Generic;
using MOPS.Events;

namespace MOPS.Tools.Generators
{
    public interface IEventGenerator
    {
        List<Event> InitializeEventsList(int numberOfEvents, SourceType sourceType);
    }
}