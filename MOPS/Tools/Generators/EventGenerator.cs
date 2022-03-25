using System.Collections.Generic;
using System.Linq;
using MOPS.Events;
using MOPS.Packages;

namespace MOPS.Tools.Generators
{
    public class EventGenerator : IEventGenerator
    {
        private readonly INumberGenerator _numberGenerator;

        public EventGenerator(INumberGenerator numberGenerator)
        {
            _numberGenerator = numberGenerator;
        }

        public List<Event> InitializeEventsList(int numberOfEvents, SourceType sourceType, int seed, int lambda)
        {
            
            List<Event> eventsList = new List<Event>();

            eventsList.Add(new Event(0, EventType.Coming, 0));
            
            for (int i = 1; i < numberOfEvents; i++)
            {
                int numberOfGeneratedEvents = _numberGenerator.Generate(sourceType, seed + i, lambda); //number of events that will arrive to the system in the span of 1 second

                double interval = 1 / (double)numberOfGeneratedEvents;
                eventsList.Add(new Event(i, EventType.Coming, eventsList.ElementAt(i-1).Time + interval));
            }

            return eventsList;
        }

        public Event CreateFinishEvent(Package package, double currentTime, double processingTime)
        {
            Event @event = new Event(package.SourceId, EventType.Finish, (float)(currentTime + processingTime + 0.001));

            return @event;
        }
    }
}