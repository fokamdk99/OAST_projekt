using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OAST.Tools
{
    public class Logs : ILogs
    {
        public Logs()
        {
        }

        public void SaveAvgWaitingTimePlot(List<double> oldTime, List<double> results)
        {
            var plot = new AvgWaitingTimePlot
            {
                OldTime = oldTime,
                WaitingAverageTime = results
            };

            string output = "";
            output += "time,";
            foreach (var time in plot.OldTime)
            {
                output += $"{time},";
            }

            output += "\nresults,";

            foreach (var result in plot.WaitingAverageTime)
            {
                output += $"{result},";
            }

            output += "\n";
            
            WriteToFile("AvgWaitingTimePlot", output);
        }
        
        public void SaveWaitingTimeVariancePlot(List<double> oldTime, List<double> results)
        {
            var plot = new WaitingTimeVariancePlot()
            {
                OldTime = oldTime,
                WaitingTimeVar = results
            };
            
            string output = "";
            output += "time,";
            foreach (var time in plot.OldTime)
            {
                output += $"{time},";
            }

            output += "\nresults,";

            foreach (var result in plot.WaitingTimeVar)
            {
                output += $"{result},";
            }

            output += "\n";
            
            WriteToFile("WaitingTimeVariancePlot", output);
        }
        
        public void SaveAvgBlockedPartPlot(List<double> oldTime, List<double> results)
        {
            var plot = new AvgBlockedPartPlot()
            {
                OldTime = oldTime,
                BlockedPartAvg = results
            };
            
            string output = "";
            output += "time,";
            foreach (var time in plot.OldTime)
            {
                output += $"{time},";
            }

            output += "\nresults,";

            foreach (var result in plot.BlockedPartAvg)
            {
                output += $"{result},";
            }

            output += "\n";
            
            WriteToFile("AvgBlockedPartPlot", output);
        }
        
        public void SaveDepartureTimeHistogram(List<double> results)
        {
            var plot = new DepartureTimeHistogram()
            {
                DepartureIntervals = results
            };
            
            string output = "";
            output += "time,";
            foreach (var time in GenerateLinspace(0, plot.DepartureIntervals.Max(), 10))
            {
                output += $"{time},";
            }

            output += "\nresults,";

            foreach (var result in plot.DepartureIntervals)
            {
                output += $"{result},";
            }

            output += "\n";
            
            WriteToFile("DepartureTimeHistogram", output);
        }

        private List<double> GenerateLinspace(double start, double stop, int num)
        {
            if (num == 1)
            {
                return new List<double> {stop};
            }

            var step = (stop - start) / (num - 1);

            List<double> steps = new List<double>();

            foreach (var i in Enumerable.Range(0, num))
            {
                steps.Add(start + step + i);
            }

            return steps;
        }
        public void WriteToFile(String p, String log)
        {
            string path = $"./logs/{p}.txt";
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(log);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(log);
                }
            }
        }
    }

    public class AvgWaitingTimePlot
    {
        public List<double> OldTime { get; set; }
        public List<double> WaitingAverageTime { get; set; }
    }
    
    public class WaitingTimeVariancePlot
    {
        public List<double> OldTime { get; set; }
        public List<double> WaitingTimeVar { get; set; }
    }
    
    public class AvgBlockedPartPlot
    {
        public List<double> OldTime { get; set; }
        public List<double> BlockedPartAvg { get; set; }
    }
    
    public class DepartureTimeHistogram
    {
        public List<double> DepartureIntervals { get; set; }
    }
}
