using System.Collections.Generic;

namespace OAST.Tools
{
    public interface IStatisticAggregator
    {
        List<IStatistic> Statistics { get; set; }
        void AddStatistic(IStatistic statistic);
        void Calculate();
    }
}