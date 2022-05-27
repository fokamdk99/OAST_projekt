using System.Collections.Generic;

namespace OAST.Server
{
    public interface ICustomServer
    {
        public int QueueSize { get; set; }
        int Mi { get; set; }
        List<int> Queue { get; set; }
        void SetMi(int mi);
        void Reset();
        int Get();
        void SetQueueSize(int queueSize);
    }
}