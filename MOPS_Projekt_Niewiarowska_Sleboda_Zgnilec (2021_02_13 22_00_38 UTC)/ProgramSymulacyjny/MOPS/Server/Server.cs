using MOPS.Tools;

namespace MOPS.Server
{
   public class CustomServer : ICustomServer
    {
        public bool Busy { get; set; }
        public int BitRate { get; set; }
        public double BusyStart { set; get; }
        public double BusyStop { set; get; }


        public CustomServer()
        {

        }

        public CustomServer(int bitRate)
        {
            Busy = false;
            BitRate = bitRate;
        }


        public void SetBusy()
        {
            Busy = true;
            BusyStart = Statistic.Time;
              
        }

        public void SetAvailable()
        {
            Busy = false;
            BusyStop = Statistic.Time;
            Statistic.CalculateServerLoadTime(BusyStart, BusyStop);
        }

        public void Reset()
        {
            Busy = false;
            BusyStart = 0;
            BusyStop = 0;
        }
    }
}
