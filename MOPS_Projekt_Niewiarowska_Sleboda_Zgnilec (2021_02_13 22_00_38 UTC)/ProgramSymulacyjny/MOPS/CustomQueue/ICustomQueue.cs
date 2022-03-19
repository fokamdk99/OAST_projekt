using System.Collections.Generic;
using MOPS.Packages;

namespace MOPS.CustomQueue
{
    public interface ICustomQueue
    {
        List<Package> Queue { get; set; }
        int QueueSize { get; set; }
        
        void Put(Package package);
        Package Get();
    }
}