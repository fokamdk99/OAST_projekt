namespace MOPS.Tools
{
    public class GlobalStatistic
    {

        public int NumberOfRecivedPackage = 0;

        public int NumberOfLostPackage = 0;

        public int NumberOfPackageinQueue = 0;

        public int packagesInSimulation = 0;

        public double averageTimeinQueue = 0;

        public double averagePackageInQueue = 0;

        public double simulationTime = 0;

        public double serverLoad = 0;

        public GlobalStatistic()
        {
            NumberOfRecivedPackage = Statistic.NumberOfReceivedPackages;
            NumberOfLostPackage = Statistic.NumberOfLostPackages;
            NumberOfPackageinQueue = Statistic.NumberOfPackagesinQueue;
            packagesInSimulation = Statistic.PackagesInSimulation;
            averageTimeinQueue = Statistic.AverageTimeinQueue;
            averagePackageInQueue = Statistic.AveragePackageInQueue;
            simulationTime = Statistic.SimulationTime;
            serverLoad = Statistic.ServerLoad;
        }
    }
}
