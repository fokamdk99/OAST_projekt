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
            NumberOfRecivedPackage = Statistic.NumberOfRecivedPackage;
            NumberOfLostPackage = Statistic.NumberOfLostPackage;
            NumberOfPackageinQueue = Statistic.NumberOfPackageinQueue;
            packagesInSimulation = Statistic.packagesInSimulation;
            averageTimeinQueue = Statistic.averageTimeinQueue;
            averagePackageInQueue = Statistic.averagePackageInQueue;
            simulationTime = Statistic.simulationTime;
            serverLoad = Statistic.serverLoad;
        }
    }
}
