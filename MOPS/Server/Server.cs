using MOPS.Tools;
using MOPS.Tools.Generators;

namespace MOPS.Server
{
   public class CustomServer : ICustomServer
    {
        private readonly INumberGenerator _numberGenerator;
        
        public bool Busy { get; set; }
        public double BusyStart { set; get; }
        public double BusyStop { set; get; }
        public int Mi { get; set; }


        public CustomServer(INumberGenerator numberGenerator)
        {
            _numberGenerator = numberGenerator;
            Busy = false;
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

        public void SetMi(int mi)
        {
            Mi = mi;
        }

        public double GenerateProcessingTime(SourceType sourceType, int seed)
        {
            int numberOfGeneratedEvents = _numberGenerator.Generate(sourceType, seed, Mi); //number of events that will arrive to the system in the span of 1 second

            return (1/(double)numberOfGeneratedEvents);
        }
    }
}
