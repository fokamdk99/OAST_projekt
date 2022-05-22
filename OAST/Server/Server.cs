using OAST.Tools;
using OAST.Tools.Generators;

namespace OAST.Server
{
   public class CustomServer : ICustomServer
    {
        private readonly INumberGenerator _numberGenerator;
        private readonly IServerMeasurements _serverMeasurements;
        private readonly IStatistic _statistic;
        
        public bool Busy { get; set; }
        public double BusyStart { set; get; }
        public double BusyStop { set; get; }
        public int Mi { get; set; }
        public CustomServer(INumberGenerator numberGenerator, 
            IServerMeasurements serverMeasurements, 
            IStatistic statistic)
        {
            _numberGenerator = numberGenerator;
            _serverMeasurements = serverMeasurements;
            _statistic = statistic;
            Busy = false;
        }

        public void SetBusy()
        {
            Busy = true;
            BusyStart = _statistic.Time;
              
        }

        public void SetAvailable()
        {
            Busy = false;
            BusyStop = _statistic.Time;
            _serverMeasurements.CalculateServerLoadTime(BusyStart, BusyStop);
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
            return  _numberGenerator.Generate(sourceType, seed, Mi); //number of events that will arrive to the system in the span of 1 second
        }
    }
}
