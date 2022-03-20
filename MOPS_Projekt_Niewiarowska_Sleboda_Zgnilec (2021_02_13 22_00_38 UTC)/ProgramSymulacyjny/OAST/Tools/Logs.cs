using System;
using System.Collections.Generic;
using System.IO;
using OAST.Events;

namespace OAST.Tools
{
    public class Logs
    {
      public static void SaveONOFFInputParameters()
        {
            String log;

            log = $"Number od sources: {Parameters.numberOfSources}\n\n Queue size: {Parameters.queueSize} \n\n [INPUT PARAMETERS]\n Package size: {Parameters.packageSize}\n OFF time: {Parameters.OFFtime}\n Number of packages: {Parameters.numberOfPackages}\n Peak rate: {Parameters.peakRate}\n\n";

            WriteToFile("SourceInfo",log);
        }

        public static void SaveCBRInputParameters()
        {
            String log;

            log = $"Number od sources: {Parameters.numberOfSources}\n\n Queue size: {Parameters.queueSize} \n\n [INPUT PARAMETERS]\n Package size: {Parameters.packageSize}\n Time between packages: {Parameters.timeBetweenPackages}\n Number of packages: {Parameters.numberOfPackages}\n Peak rate: {Parameters.peakRate}\n\n";
            
            WriteToFile("SourceInfo",log);
        }

        public static void SaveServerParameters()
        {
            String log;

            log = $"[SERVER PARAMETERS]\nServer Bitrate: {Parameters.serverBitRate}\n Server time: {Parameters.serverTime} \n\n";
            
            WriteToFile("Log", log);
        }

        public static void SaveEventList(List<Event> list)
        {
            String log = "";
            String tmp;
            
            for (int i = 0; i < list.Count; i++)
            {
               tmp = "Source ID: " + list[i].sourceID + " Time: " + list[i].time + "Type: " + list[i].type + "\n";
               log = log + tmp;
               tmp = "";
            }
            log = log + "\n";

            WriteToFile("EventList",log);
        }

        public static void SaveStatistic()
        {
            String log;
            
            log = $"[STATISTIC PARAMETERS]\nLost: {Statistic.NumberOfLostPackage}\nReceived: {Statistic.NumberOfRecivedPackage} \nPackages In Simulation: {Statistic.packagesInSimulation}\nAverage Time in Queue: {Statistic.averageTimeinQueue}\nAverage Package In Queue: {Statistic.averagePackageInQueue}\n simulation Time: {Statistic.simulationTime}\nServer Load: {Statistic.serverLoad}\nPercent Of Success: {Statistic.percentOfSuccess}" ;
            
            WriteToFile("Log",log);
        }

        public static void SaveAverageTimeinQueue()
        {
            String log;

            log = $"[AVERAGE TIME IN QUEUE]\n{Statistic.averageTimeinQueue}\n\n";
            
            WriteToFile("",log);
        }

        public static void SaveLog()
        {
            String log;

            log = $"{Statistic.NumberOfLostPackage}";
            
            WriteToFile("Test", log);
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
