using System.Collections.Generic;
using System.Linq;
using MOPS.Events;
using MOPS.Packages;

namespace MOPS.Queue
{
    public class CustomQueue : ICustomQueue
    {
        public List<Event> EventsList { get; set; }
        public List<Package> Queue { get; set; }
        public int QueueSize { get; set; }

        public CustomQueue()
        {
            EventsList = new List<Event>();
            Queue = new List<Package>();
        }

        public CustomQueue(int queueSize) : base()
        {
            QueueSize = queueSize;
        }
        
        public void Put(Package package)
        {
            Queue.Add(package);
        }

        public Package Get()
        {
            return Queue.ElementAt(0);
        }

        public void Sort()
        {
            EventsList.Sort((x, y) => x.Time.CompareTo(y.Time));
        }
    }
}