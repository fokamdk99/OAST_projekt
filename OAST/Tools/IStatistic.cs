namespace OAST.Tools
{
    public interface IStatistic
    {
        int NumberOfReceivedPackages { get; set; }
        int NumberOfLostPackages { get; set; }
        double Time { get; set; }
        double SimulationTime { get; set; }
        double PercentOfSuccess { get; set; }

        void ResetStatistics();
        void IncrementNumberOfReceivedPackages();
        void IncrementNumberOfLostPackages();
        void PrintStatistics();
    }
}