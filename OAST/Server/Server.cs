using System.Collections.Generic;
using System.Linq;
using OAST.Tools;
using OAST.Tools.Generators;

namespace OAST.Server
{
   public class CustomServer : ICustomServer
    {
        public bool Busy { get; set; }
        public double BusyStart { set; get; }
        public double BusyStop { set; get; }
        public int Mi { get; set; }
        public List<int> Queue { get; set; }


        public CustomServer()
        {
            Busy = false;
            Queue = new List<int>();
        }

        public int Get()
        {
            var result = Queue.ElementAt(0);
            Queue.RemoveAt(0);
            return result;
        }

        public void Reset()
        {
            Busy = false;
            BusyStart = 0;
            BusyStop = 0;
        }

        public void SetMi(int mi)
        {
            Mi = mi;
        }
    }
}
