using System.Collections.Generic;

namespace OAST.Server
{
    public interface ICustomServer
    {
        public int QueueSize { get; set; }
        double Mi { get; set; }
        List<int> Queue { get; set; }
        void SetMi(double mi);
        void Reset();
        int Get();
        void SetQueueSize(int queueSize);
    }
}