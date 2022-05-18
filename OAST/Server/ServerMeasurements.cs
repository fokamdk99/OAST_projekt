using System;

namespace OAST.Server
{
    public class ServerMeasurements : IServerMeasurements
    {
        public double ServerLoad { get; set; }
        public double ServerLoadTime { get; set; }
        
        public int ProcessedPackages { get; set; }
        
        public void Reset()
        {
            ServerLoad = 0;
            ServerLoadTime = 0;
            ProcessedPackages = 0;
        }

        public void IncrementProcessedPackages()
        {
            ProcessedPackages += 1;
        }
        
        public double CalculateServerLoad(double simulationTime)
        {
            ServerLoad = ServerLoadTime / simulationTime;
            return ServerLoad;
        }
        public double CalculateAverageServerProcessingTime()
        {
            return ServerLoadTime / ProcessedPackages;
            
        }

        public void CalculateServerLoadTime(double busyStart, double busyStop)
        {
            ServerLoadTime += (busyStop - busyStart);
        }
        
        public void PrintServerLoad(double simulationTime)
        {
            Console.WriteLine($"[SERVER] \n Processed packages: {ProcessedPackages}\n Processing time: {ServerLoadTime}\nAverage of processing time: {CalculateAverageServerProcessingTime()}\n");
        }
    }
}