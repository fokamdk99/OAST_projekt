namespace OAST.Tools
{
    public interface IAggregateMeasurements
    {
        int NumberOfReceivedPackages { get; set; }
        int NumberOfLostPackages { get; set; }
        int NumberOfPackagesInQueue { get; set; }
        double AverageTimeInQueue { get; set; }
        double AverageNumberOfPackagesInQueue { get; set; }
        double SimulationTime { get; set; }
        double ServerLoad { get; set; }
        double PercentOfSuccess { get; set; }
    }
}