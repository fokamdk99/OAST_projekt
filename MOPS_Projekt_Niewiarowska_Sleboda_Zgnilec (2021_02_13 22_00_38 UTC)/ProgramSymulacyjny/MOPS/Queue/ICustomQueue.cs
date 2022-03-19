using System.Collections.Generic;
using MOPS.Events;
using MOPS.Packages;

namespace MOPS.Queue
{
    public interface ICustomQueue
    {
        List<Event> EventsList { get; set; }
        List<Package> Queue { get; set; }
        int QueueSize { get; set; }
        
        void Put(Package package);
        Package Get();
        void Sort();
    }
}