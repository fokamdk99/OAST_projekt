namespace MOPS.Server
{
    public interface IServer
    {
        bool Busy { get; set; }
        int BitRate { get; set; }
        double BusyStart { set; get; }
        double BusyStop { set; get; }
    }
}