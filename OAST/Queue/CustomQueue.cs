using System;
using System.Collections.Generic;
using System.Linq;
using OAST.Events;
using OAST.OASTPackages;
using OAST.Tools.Generators;

namespace OAST.Queue
{
    public class CustomQueue : ICustomQueue
    {
        private readonly IEventGenerator _eventGenerator;
        
        public List<Event> EventsList { get; set; }
        public List<Package> Queue { get; set; }
        public int SortingIndicator { get; set; }
        public int MaxSortLength { get; set; }
        public int MaxQueueLength { get; set; }
        private int QueueSize { get; set; }
        private int NumberOfProcessedEvents { get; set; } // to chyba do wywalenia 

        public CustomQueue(IEventGenerator eventGenerator)
        {
            _eventGenerator = eventGenerator;
            EventsList = new List<Event>();
            Queue = new List<Package>();
            SortingIndicator = 0;
            MaxSortLength = 0;
            MaxQueueLength = 0;
        }

        public CustomQueue(int queueSize, IEventGenerator eventGenerator) : base()
        {
            QueueSize = queueSize;
            _eventGenerator = eventGenerator;
        }
        
        public void Put(Package package)
        {
            Queue.Add(package);
            MaxQueueLength = Math.Max(MaxQueueLength, Queue.Count);
        }

        public Package Get()
        {
            return Queue.ElementAt(0);
        }

        public void RemovePackageFromQueue()
        {
            Queue.RemoveAt(0);
        }

        public void Sort()
        {
            MaxSortLength = Math.Max(MaxSortLength, EventsList.Count - SortingIndicator);
            EventsList
                .Sort(SortingIndicator, EventsList.Count-SortingIndicator, new EventsComparer());
        }

        public void Reset()
        {
            Queue = new List<Package>();
            EventsList = new List<Event>();
            NumberOfProcessedEvents = 0;
            SortingIndicator = 0;
        }

        public void InitializeEventsList(int numberOfEvents, SourceType eventType, int seed, int lambda)
        {
            EventsList = _eventGenerator.InitializeEventsList(numberOfEvents, eventType, seed, lambda);
            Sort();
        }

        public void SetQueueSize(int queueSize)
        {
            QueueSize = queueSize;
        }

        public void IncrementNumberOfProcessedEvents()
        {
            NumberOfProcessedEvents += 1;
        }

        public int GetNumberOfProcessedEvents()
        {
            return NumberOfProcessedEvents;
        }

        public void ShowQueueMeasurements()
        {
            Console.WriteLine($"SortingIndicator: {SortingIndicator}\n" +
                              $"MaxSortLength: {MaxSortLength}\n" +
                              $"MaxQueueLength: {MaxQueueLength}\n" +
                              $"All events : {NumberOfProcessedEvents} = numberOfPackages x 2");
        }
    }
}