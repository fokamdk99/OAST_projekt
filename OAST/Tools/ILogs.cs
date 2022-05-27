using System;
using System.Collections.Generic;

namespace OAST.Tools
{
    public interface ILogs
    {
        void WriteToFile(String p, String log);
        void SaveAvgWaitingTimePlot(List<double> oldTime, List<double> results);
        void SaveWaitingTimeVariancePlot(List<double> oldTime, List<double> results);
        void SaveAvgBlockedPartPlot(List<double> oldTime, List<double> results);
        void SaveDepartureTimeHistogram(List<double> results);
    }
}