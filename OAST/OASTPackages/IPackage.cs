namespace OAST.OASTPackages
{
    public interface IPackage
    {
        int Id { get; set; }
        int SourceId { get; set; }
        double ComingTime { get; set; }
        double AddToQueueTime { get; set; }
        double GetFromQueueTime { get; set; }
        double FinishTime { get; set; }
    }
}