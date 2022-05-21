using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OAST.Events;
using OAST.Queue;
using OAST.Server;

namespace OAST.Tools
{
    public class Logs : ILogs
    {
        private readonly IStatistic _statistic;
        private readonly IQueueMeasurements _queueMeasurements;
        private readonly IServerMeasurements _serverMeasurements;

        public Logs(IStatistic statistic, 
            IQueueMeasurements queueMeasurements, 
            IServerMeasurements serverMeasurements)
        {
            _statistic = statistic;
            _queueMeasurements = queueMeasurements;
            _serverMeasurements = serverMeasurements;
        }

        public void SaveEventList(List<Event> list)
        {
            String log = "";
            String tmp;
            
            for (int i = 0; i < list.Count; i++)
            { 
                // source Id, time, event type
               tmp = list[i].SourceId + "," + list[i].Time + "," + (int) list[i].Type + "\n";
               log = log + tmp;
               tmp = "";
            }
            log = log + "\n";

            WriteToFile("EventList",log);
        }

        public void SaveVariances(List<double> variances)
        {
            String log = "";

            foreach (var item in variances.Select((value, i) => new {i, value}))
            {
                log += item.i;
                log += ",";
                log += item.value;
            }
            
            WriteToFile("Variances",log);
        }

        public void SaveStatistic()
        {
            String log;
            
            log = $"[STATISTIC PARAMETERS]\nLost: {_statistic.NumberOfLostPackages}\nReceived: {_statistic.NumberOfReceivedPackages} \nAverage Time in Queue: {_queueMeasurements.TimeInQueue}\nAverage Package In Queue: {_queueMeasurements.AverageNumberOfPackagesInQueue}\n simulation Time: {_statistic.SimulationTime}\nServer Load: {_serverMeasurements.ServerLoad}\nPercent Of Success: {_statistic.PercentOfSuccess}" ;
            
            WriteToFile("Log",log);
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
}
