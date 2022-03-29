using System.Collections.Generic;
using System.Linq;
using OAST.Events;
using OAST.Packages;

namespace OAST.Tools.Generators
{
    public class EventGenerator : IEventGenerator
    {
        private readonly INumberGenerator _numberGenerator;
        public int NumberOfEvents { get; set; }
        public int NumberOfCreatedEvents { get; set; }
        public double LastGeneratedTime { get; set; }

        public EventGenerator(INumberGenerator numberGenerator)
        {
            _numberGenerator = numberGenerator;
            NumberOfEvents = 0;
            NumberOfCreatedEvents = 0;
            LastGeneratedTime = 0;
        }

        public List<Event> InitializeEventsList(int numberOfEvents, SourceType sourceType, int seed, int lambda)
        {
            NumberOfEvents = numberOfEvents;
            
            List<Event> eventsList = new List<Event>();

            if (numberOfEvents > 10)
            {
                numberOfEvents = 10;
            }

            eventsList = GenerateEvents(sourceType, seed, lambda, numberOfEvents);
            
            return eventsList;
        }

        public List<Event> CreateEvents(SourceType sourceType, int seed, int lambda)
        {
            var eventsList = GenerateEvents(sourceType, seed, lambda);
            
            return eventsList;
        }

        private List<Event> GenerateEvents(SourceType sourceType, int seed, int lambda, int numberOfEvents = 2)
        {
            List<Event> eventsList = new List<Event>();
            
            eventsList.Add(new Event(NumberOfCreatedEvents, EventType.Coming, LastGeneratedTime + CalculateInterval(sourceType, seed, lambda, 0)));
            
            NumberOfCreatedEvents += 1;
            
            for (int i = 1; i < numberOfEvents; i++)
            {
                var interval = CalculateInterval(sourceType, seed, lambda, i);
                eventsList.Add(new Event(NumberOfCreatedEvents, EventType.Coming, eventsList.ElementAt(i-1).Time + interval));
                NumberOfCreatedEvents += 1;
            }
            
            LastGeneratedTime = eventsList.Last().Time;

            return eventsList;
        }

        private double CalculateInterval(SourceType sourceType, int seed, int lambda, int i)
        {
            int numberOfGeneratedEvents =
                _numberGenerator.Generate(sourceType, seed + i,
                    lambda); //number of events that will arrive to the system in the span of 1 second
            if (numberOfGeneratedEvents == 0)
            {
                numberOfGeneratedEvents = lambda;
            }

            double interval = 1 / (double) numberOfGeneratedEvents;
            return interval;
        }

        public Event CreateFinishEvent(Package package, double currentTime, double processingTime)
        {
            Event @event = new Event(package.SourceId, EventType.Finish, (float)(currentTime + processingTime));

            return @event;
        }
    }
}