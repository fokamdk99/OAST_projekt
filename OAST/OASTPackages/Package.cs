namespace OAST.OASTPackages
{
    public class Package : IPackage
    {
        public int Id { get; set; }
        public int SourceId { get; set; }
        public double ComingTime { get; set; }
        public double AddToQueueTime { get; set; }
        public double GetFromQueueTime { get; set; }
        public double FinishTime { get; set; }
        public Package()
        {
        }

        public Package(int id, int sourceId, double time)
        {
            Id = id;
            ComingTime = time;
            AddToQueueTime = 0;
            GetFromQueueTime = 0;
            FinishTime = 0;
            SourceId = sourceId;
        }
    }
}