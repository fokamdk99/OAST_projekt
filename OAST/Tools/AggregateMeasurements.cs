namespace OAST.Tools
{
    public class AggregateMeasurements : IAggregateMeasurements
    {
        public int NumberOfReceivedPackages { get; set; }
        public int NumberOfLostPackages { get; set; }
        public int NumberOfPackagesInQueue { get; set; }
        public double AverageTimeInQueue { get; set; }
        public double AverageNumberOfPackagesInQueue { get; set; }
        public double SimulationTime { get; set; }
        public double ServerLoad { get; set; }
        public double PercentOfSuccess { get; set; }

        public AggregateMeasurements()
        {
            
        }

        public AggregateMeasurements(int numberOfReceivedPackages,
            int numberOfLostPackages,
            int numberOfPackagesInQueue,
            double averageTimeInQueue,
            double averageNumberOfPackagesInQueue,
            double simulationTime,
            double serverLoad)
        {
            NumberOfReceivedPackages = numberOfReceivedPackages;
            NumberOfLostPackages = numberOfLostPackages;
            NumberOfPackagesInQueue = numberOfPackagesInQueue;
            AverageTimeInQueue = averageTimeInQueue;
            AverageNumberOfPackagesInQueue = averageNumberOfPackagesInQueue;
            SimulationTime = simulationTime;
            ServerLoad = serverLoad;
        }
    }
}
