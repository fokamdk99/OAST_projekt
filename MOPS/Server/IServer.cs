namespace MOPS.Server
{
    public interface ICustomServer
    {
        bool Busy { get; set; }
        int BitRate { get; set; }
        double BusyStart { set; get; }
        double BusyStop { set; get; }
        void SetBusy();
        void SetAvailable();
        void Reset();
    }
}