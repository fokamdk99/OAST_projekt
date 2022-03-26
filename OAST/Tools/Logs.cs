using System;
using System.Collections.Generic;
using System.IO;
using OAST.Events;

namespace OAST.Tools
{
    public class Logs
    {
        public static void SaveEventList(List<Event> list)
        {
            String log = "";
            String tmp;
            
            for (int i = 0; i < list.Count; i++)
            {
               tmp = "Source ID: " + list[i].SourceId + " Time: " + list[i].Time + "Type: " + list[i].Type + "\n";
               log = log + tmp;
               tmp = "";
            }
            log = log + "\n";

            WriteToFile("EventList",log);
        }

        public static void SaveStatistic()
        {
            String log;
            
            log = $"[STATISTIC PARAMETERS]\nLost: {Statistic.NumberOfLostPackages}\nReceived: {Statistic.NumberOfReceivedPackages} \nPackages In Simulation: {Statistic.PackagesInSimulation}\nAverage Time in Queue: {Statistic.AverageTimeinQueue}\nAverage Package In Queue: {Statistic.AveragePackageInQueue}\n simulation Time: {Statistic.SimulationTime}\nServer Load: {Statistic.ServerLoad}\nPercent Of Success: {Statistic.PercentOfSuccess}" ;


            WriteToFile("Log",log);
        }

        private static void WriteToFile(String p, String log)
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
}
