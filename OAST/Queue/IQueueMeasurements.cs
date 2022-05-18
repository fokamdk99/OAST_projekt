namespace OAST.Queue
{
    public interface IQueueMeasurements
    {
        double TimeinQueue { get; set; }
        double AverageNumberOfPackagesInQueue { get; set; }
        int NumberOfPackagesThatWereQueue { get; set; }
        int NumberOfPackagesThatWereNotQueue { get; set; }

        void Reset();
        void AddTimeInQueue(double time);
        double CalculateAverageTime();
        void IncrementNumberOfPackagesThatWereInQueue();
        void PrintAverageTimeInQueue();
        void IncrementNumberOfPackagesThatWereNotInQueue();
    }
}