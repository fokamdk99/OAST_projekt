using System;
using System.Collections.Generic;
using System.Linq;

namespace OAST.Tools
{
    public class Statistic : IStatistic
    {
        public List<bool> Blocked { get; set; }
        public List<double?> ArrivalTime { get; set; }
        public List<double?> ServiceStartTime { get; set; }
        public List<double?> DepartureTime { get; set; }
        public List<int?> FillOnArrival { get; set; }

        public Statistic()
        {
            Blocked = new List<bool>();
            ArrivalTime = new List<double?>();
            ServiceStartTime = new List<double?>();
            DepartureTime = new List<double?>();
            FillOnArrival = new List<int?>();
        }

        public int NewEntry()
        {
            int id = Blocked.Count;
            Blocked.Add(default);
            ArrivalTime.Add(null);
            ServiceStartTime.Add(null);
            DepartureTime.Add(null);
            FillOnArrival.Add(null);
            return id;
        }
        
        public double GetMeanBlockedPart()
        {
            var numberOfBlocked = Blocked.Count(x => x);
            return (double) numberOfBlocked / Blocked.Count;
        }

        public List<double> GetMeanBatchedBlockedPart(double period)
        {
            var timeLimit = period;
            int blocked = 0;
            int allN = 0;
            List<double> blockedPartAll = new List<double>();

            foreach (var arrivalTime in ArrivalTime.Select((value, index) => new {value, index}))
            {
                if (arrivalTime.value > timeLimit)
                {
                    if (allN == 0)
                    {
                        blockedPartAll.Add(0);
                    }
                    else
                    {
                        blockedPartAll.Add((double)blocked/allN);
                    }

                    blocked = 0;
                    allN = 0;
                    timeLimit += period;
                }
                else
                {
                    if (Blocked.ElementAt(arrivalTime.index))
                    {
                        blocked += 1;
                        allN += 1;
                    }
                    else
                    {
                        allN += 1;
                    }
                }
            }

            if (allN == 0)
            {
                blockedPartAll.Add(0);
            }
            else
            {
                blockedPartAll.Add((double) blocked/allN);
            }
            
            return blockedPartAll;
        }

        public List<double> GetMeanBatchedWaitingTime(double period)
        {
            List<double> waitingTimeAll = new List<double>();
            double avgTime = 0;
            double avgTimeN = 0;
            double avgTimeLimit = period;
            foreach (var arrivalTime in ArrivalTime.Select((value, index) => new {value, index}))
            {
                if (Blocked.ElementAt(arrivalTime.index) || arrivalTime.value == null || ServiceStartTime.ElementAt(arrivalTime.index) == null) //TODO: co zrobic z reszta warunkow?
                {
                    continue;
                }

                if (arrivalTime.value > avgTimeLimit)
                {
                    waitingTimeAll.Add(avgTime);
                    avgTime = 0;
                    avgTimeN = 0;
                    avgTimeLimit += period;
                }

                if (avgTimeN == 0)
                {
                    avgTime = (double) (ServiceStartTime.ElementAt(arrivalTime.index) - arrivalTime.value);
                    avgTimeN = 1;
                }
                else
                {
                    avgTime = (double)(avgTime + (ServiceStartTime.ElementAt(arrivalTime.index) - arrivalTime.value - avgTime) /
                        avgTimeN);
                    avgTimeN += 1;
                }
            }
            
            waitingTimeAll.Add(avgTime);
            return waitingTimeAll;
        }

        public List<double> GetVarianceBatchedWaitingTime(double period, double globalAverage)
        {
            List<double> varAll = new List<double>();
            List<double> varSamples = new List<double>();
            double varTimeLimit = period;
            foreach (var arrivalTime in ArrivalTime.Select((value, index) => new {value, index}))
            {
                if (Blocked.ElementAt(arrivalTime.index) || arrivalTime.value == null || ServiceStartTime.ElementAt(arrivalTime.index) == null) //TODO: co z pozostalymi warunkami?
                {
                    continue;
                }

                if (arrivalTime.value > varTimeLimit)
                {
                    if (varSamples.Count == 0)
                    {
                        varAll.Add(0);
                    }
                    else
                    {
                        varAll.Add(varSamples.Sum(x => Math.Pow(x - globalAverage, 2))/varSamples.Count);
                    }

                    varTimeLimit += period;
                    varSamples.Clear();
                }
                else
                {
                    varSamples.Add((double)(ServiceStartTime.ElementAt(arrivalTime.index) - arrivalTime.value));
                }
            }

            return varAll;
        }
    }
}