using System;
using System.Collections.Generic;
using OAST.Events;

namespace OAST.Tools
{
    public interface ILogs
    {
        void SaveEventList(List<Event> list);
        void SaveStatistic();
        void WriteToFile(String p, String log);
        void SaveVariances(List<double> variances);
    }
}