namespace OAST.Queue
{
    public interface IQueueMeasurements
    {
        double AverageTimeinQueue { get; set; }
        double AverageNumberOfPackagesInQueue { get; set; }
        int NumberOfPackagesInQueue { get; set; }

        void Reset();
        void AddAverageTimeinQueue(double time);
        double CalculateAverageTime();
        void IncrementNumberOfPackagesInQueue();
        void PrintAverageTimeInQueue();
    }
}