using System.Collections.Generic;
using System.Linq;

namespace OAST.Server
{
   public class CustomServer : ICustomServer
    {
        public int Mi { get; set; }
        public List<int> Queue { get; set; }
        public int QueueSize { get; set; }


        public CustomServer()
        {
            Queue = new List<int>();
            QueueSize = 0;
        }

        public void SetQueueSize(int queueSize)
        {
            QueueSize = queueSize;
        }

        public int Get()
        {
            var result = Queue.ElementAt(0);
            Queue.RemoveAt(0);
            return result;
        }

        public void Reset()
        {
            Queue.Clear();
        }

        public void SetMi(int mi)
        {
            Mi = mi;
        }
    }
}
