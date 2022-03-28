namespace OAST.Tools
{
    public class GlobalStatistic
    {

        public int NumberOfReceivedPackages = 0;

        public int NumberOfLostPackages = 0;

        public int NumberOfPackagesInQueue = 0;
        
        public double AverageTimeInQueue = 0;

        public double AverageNumberOfPackagesInQueue = 0;

        public double SimulationTime = 0;

        public double ServerLoad = 0;

        public GlobalStatistic()
        {
            NumberOfReceivedPackages = Statistic.NumberOfReceivedPackages;
            NumberOfLostPackages = Statistic.NumberOfLostPackages;
            NumberOfPackagesInQueue = Statistic.NumberOfPackagesInQueue;
            AverageTimeInQueue = Statistic.AverageTimeinQueue;
            AverageNumberOfPackagesInQueue = Statistic.AverageNumberOfPackagesInQueue;
            SimulationTime = Statistic.SimulationTime;
            ServerLoad = Statistic.ServerLoad;
        }
    }
}
