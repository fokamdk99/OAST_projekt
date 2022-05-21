using System;

namespace OAST.Server
{
    public class ServerMeasurements : IServerMeasurements
    {
        public double ServerLoad { get; set; }
        public double ServerLoadTime { get; set; }
        public int ProcessedPackages { get; set; }
        
        public double ProcessingTime { get; set; }
        public void AddProcessingTime(double time)
        {
            ProcessingTime += time;
        }
     
        public void Reset()
        {
            ServerLoad = 0;
            ServerLoadTime = 0;
            ProcessedPackages = 0;
            ProcessingTime = 0;
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
            double time = busyStop - busyStart;
            ServerLoadTime += time;
        }
        
        public void PrintServerLoad(double simulationTime)
        {
            Console.WriteLine($"[SERVER] \n Processed packages: {ProcessedPackages}\n " +
                              $"Server load: {ServerLoadTime}\n" +
                              $" Processing time: {ProcessingTime}\n " +
                              $"Average of processing time: {ProcessingTime/ProcessedPackages} " +
                              $"\nAverage of processing time: {CalculateAverageServerProcessingTime()}\n");
        }
    }
}