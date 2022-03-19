namespace MOPS.Packages
{
    public interface IPackage
    {
        int Id { get; set; }
        int SourceId { get; set; }
        int Size { get; set; }
        double ComingTime { get; set; }
        double AddToQueueTime { get; set; }
        double GetFromQueueTime { get; set; }
        double FinishTime { get; set; }
    }
}