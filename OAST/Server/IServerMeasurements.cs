namespace OAST.Server
{
    public interface IServerMeasurements
    {
        double ServerLoad { get; set; }

        double ServerLoadTime { get; set; }

        void Reset();
        double CalculateServerLoad(double simulationTime);
        void CalculateServerLoadTime(double busyStart, double busyStop);
        void PrintServerLoad(double simulationTime);
    }
}