namespace OAST.Server
{
    public interface IServerMeasurements
    {
        double ServerLoad { get; set; }

        double ServerLoadTime { get; set; }
        
        int ProcessedPackages { get; set; }
        public double ProcessingTime { get; set; }
        
        void Reset();
        double CalculateServerLoad(double simulationTime);
        void CalculateServerLoadTime(double busyStart, double busyStop);
        void PrintServerLoad(double simulationTime);
        void IncrementProcessedPackages();
        double CalculateAverageServerProcessingTime();
        void AddProcessingTime(double time);
    }
}