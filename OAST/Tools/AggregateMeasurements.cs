namespace OAST.Tools
{
    public class AggregateMeasurements : IAggregateMeasurements
    {
        public int NumberOfReceivedPackages { get; set; }
        public int NumberOfLostPackages { get; set; }
        
        public int NumberOfPackagesInQueue { get; set; }
        
        public int NumberOfProcessedPackages { get; set; }
        public double AverageTimeInQueue { get; set; }
        public double SimulationTime { get; set; }
        public double ServerLoad { get; set; }
        public double PercentOfSuccess { get; set; }
        public int NumberOfPackagesThatWereInQueue { get; set; }
        public double AverageNumberOfPackagesInQueue { get; set; }
        public int NumberOfPackagesThatWereNotInQueue { get; set; }
        public double ProcessingTime { get; set; }
        public double AverageProcessingTime { get; set; }
         public AggregateMeasurements()
        {
            
        }

        public AggregateMeasurements(
            int numberOfReceivedPackages,
            int numberOfLostPackages,
            int numberOfProcessedPackages,
            int numberOfPackagesThatWereInQueue,
            int numberOfPackagesThatWereNotInQueue,
            double averageTimeInQueue,
            double processingTime,
            double averageProcessingTime,
            double simulationTime)
        {
            NumberOfReceivedPackages = numberOfReceivedPackages;
            NumberOfLostPackages = numberOfLostPackages;
            NumberOfProcessedPackages = numberOfProcessedPackages;
            NumberOfPackagesThatWereInQueue = numberOfPackagesThatWereInQueue;
            NumberOfPackagesThatWereNotInQueue = numberOfPackagesThatWereNotInQueue;
            AverageTimeInQueue = averageTimeInQueue;
            ProcessingTime = processingTime;
            AverageProcessingTime = averageProcessingTime;
            SimulationTime = simulationTime;
        }
    }
}
