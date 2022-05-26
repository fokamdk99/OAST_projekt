using System.Collections.Generic;

namespace OAST.Tools
{
    public interface IStatistic
    {
        List<bool> Blocked { get; set; }
        List<double?> ArrivalTime { get; set; } //ok
        List<double?> ServiceStartTime { get; set; } //ok
        List<double?> DepartureTime { get; set; }
        List<int?> FillOnArrival { get; set; }
        List<double> GetMeanBatchedWaitingTime(double period);
        List<double> GetMeanBatchedBlockedPart(double period);
        List<double> GetVarianceBatchedWaitingTime(double period, double globalAverage);
        int NewEntry();

    }
}