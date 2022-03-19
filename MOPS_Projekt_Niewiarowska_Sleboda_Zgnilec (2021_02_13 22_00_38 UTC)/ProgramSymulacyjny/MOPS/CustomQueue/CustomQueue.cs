using System.Collections.Generic;
using System.Linq;
using MOPS.Packages;

namespace MOPS.CustomQueue
{
    public class CustomQueue : ICustomQueue
    {
        public List<Package> Queue { get; set; }
        public int QueueSize { get; set; }
        
        public void Put(Package package)
        {
            Queue.Add(package);
        }

        public Package Get()
        {
            return Queue.ElementAt(0);
        }
    }
}