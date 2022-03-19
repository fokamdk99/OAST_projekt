using MOPS.Tools;

namespace MOPS.Server
{
   public class Server : IServer
    {
        public bool Busy { get; set; }
        public int BitRate { get; set; }
        public double BusyStart { set; get; }
        public double BusyStop { set; get; }


        public Server()
        {

        }

        public Server(int bitRate)
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
            Statistic.calculateServerLoadTime(BusyStart, BusyStop);
        }
    }
}
