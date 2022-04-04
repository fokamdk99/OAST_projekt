using System;

namespace OAST.Server
{
    public class ServerMeasurements : IServerMeasurements
    {
        public double ServerLoad { get; set; }
        public double ServerLoadTime { get; set; }
        
        public void Reset()
        {
            ServerLoad = 0;
            ServerLoadTime = 0;
        }
        
        public double CalculateServerLoad(double simulationTime)
        {
            ServerLoad = ServerLoadTime / simulationTime;
            return ServerLoad;
        }

        public void CalculateServerLoadTime(double busyStart, double busyStop)
        {
            ServerLoadTime = ServerLoadTime + (busyStop - busyStart);
        }
        
        public void PrintServerLoad(double simulationTime)
        {
            Console.WriteLine($"[Server Load] {CalculateServerLoad(simulationTime)}\n\n");
        }
    }
}