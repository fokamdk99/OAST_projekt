using System.Collections.Generic;
using System.Linq;

namespace OAST.Tools
{
    public class StatisticAggregator : IStatisticAggregator
    {
        private readonly IParameters _parameters;
        public List<IStatistic> Statistics { get; set; }

        public StatisticAggregator(IParameters parameters)
        {
            _parameters = parameters;
            Statistics = new List<IStatistic>();
        }

        public void Calculate()
        {
            var departuresInterval = SelectDeparturesInterval();
            var timeSamples = SelectTimeSamples();
            var waitingTimeAll = GetWaitingTimeAll();
            var blockedPartAll = GetBlockedPartAll();
            var waitingTimeAvg =  GetWaitingTimeAvg(waitingTimeAll);
            var blockedPartAvg = GetBlockedPartAvg(blockedPartAll);
            var waitingTimeVarAll = GetWaitingTimeVarAll(waitingTimeAvg);
        }

        public void AddStatistic(IStatistic statistic)
        {
            Statistics.Add(statistic);
        }

        public List<List<double?>> SelectDeparturesInterval()
        {
            List<List<double?>> departuresInterval = new List<List<double?>>();
            
            foreach (var statistic in Statistics)
            {
                var x = statistic.DepartureTime.Where(x => x != null).ToList();
                List<double?> diffs = new List<double?>();
                var aggregate = x.Aggregate((prev, next) =>
                {
                    diffs.Add(next - prev);
                    return next;
                });
                departuresInterval.Add(diffs);
            }

            return departuresInterval;
        }

        public List<double> SelectTimeSamples()
        {
            List<double> waitingTimeSamplesAll = new List<double>();
            foreach (var statistic in Statistics)
            {
                for (int i = 0; i < statistic.ArrivalTime.Count; i++)
                {
                    if (statistic.ServiceStartTime[i] != null)
                    {
                        waitingTimeSamplesAll.Add((double)(statistic.ServiceStartTime[i] - statistic.ArrivalTime[i]));
                    }
                }
            }

            return waitingTimeSamplesAll;
        }

        public List<List<double>> GetWaitingTimeAll()
        {
            List<List<double>> waitingTimeAll = new List<List<double>>();

            foreach (var statistic in Statistics)
            {
                var waitingTime = statistic.GetMeanBatchedWaitingTime(_parameters.BlockSize);
                waitingTimeAll.Add(waitingTime);
            }

            return waitingTimeAll;
        }
        
        public List<List<double>> GetBlockedPartAll()
        {
            List<List<double>> blockedPartAll = new List<List<double>>();

            foreach (var statistic in Statistics)
            {
                var blockedPart = statistic.GetMeanBatchedBlockedPart(_parameters.BlockSize);
                blockedPartAll.Add(blockedPart);
            }

            return blockedPartAll;
        }

        public List<double> GetWaitingTimeAvg(List<List<double>> waitingTimeAll)
        {
            return GetAvgFromBatchedVar(waitingTimeAll);
        }
        
        public List<double> GetBlockedPartAvg(List<List<double>> blockedPartAll)
        {
            return GetAvgFromBatchedVar(blockedPartAll);
        }

        public List<double> GetAvgFromBatchedVar(List<List<double>> batchedVar)
        {
            List<double> avgFromBatches = new List<double>();
            foreach (var batch in batchedVar)
            {
                avgFromBatches.Add(batch.Sum(x => x)/batch.Count);
            }

            return avgFromBatches;
        }

        public double GetWaitingTimeGlobalAvg(List<double> waitingTimeAvg)
        {
            return waitingTimeAvg.Average();
        }

        public List<List<double>> GetWaitingTimeVarAll(List<double> waitingTimeAvg)
        {
            List<List<double>> waitingTimeVarAll = new List<List<double>>();
            foreach (var statistic in Statistics)
            {
                waitingTimeVarAll.Add(statistic.GetVarianceBatchedWaitingTime(_parameters.BlockSize, GetWaitingTimeGlobalAvg(waitingTimeAvg)));
            }

            return waitingTimeVarAll;
        }

        public void SomeShittyStuff(List<double> waitingTimeAvg)
        {
            double temp = 0;
            List<double> time = new List<double>();
            foreach (var timeAvg in waitingTimeAvg)
            {
                time.Add(temp);
                temp += _parameters.BlockSize;
            }
            
            List<double?> diffs = new List<double?>();
            var aggregate = waitingTimeAvg.Aggregate((prev, next) =>
            {
                diffs.Add(next - prev);
                return next;
            });

            List<int> indices = new List<int>();
            var tmp = diffs.Select((x, index) =>
            {
                if (x < _parameters.CutOffWaitingTimeDiff)
                {
                    indices.Add(index);
                }

                return x;
            });

            int oldLen = waitingTimeAvg.Count;
        }
    }
}