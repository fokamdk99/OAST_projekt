using OAST.Tools.Generators;

namespace OAST.Server
{
    public interface ICustomServer
    {
        bool Busy { get; set; }
        double BusyStart { set; get; }
        double BusyStop { set; get; }
        int Mi { get; set; }
        void SetBusy();
        void SetAvailable();
        void SetMi(int mi);
        void Reset();
        double GenerateProcessingTime(SourceType sourceType, int seed);
    }
}