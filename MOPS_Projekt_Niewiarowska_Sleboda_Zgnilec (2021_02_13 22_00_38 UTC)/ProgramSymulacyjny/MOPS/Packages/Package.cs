namespace MOPS.Packages
{
    public class Package : IPackage
    {
        public int Id { get; set; }
        public int SourceId { get; set; }
        public int Size { get; set; }
        public double ComingTime { get; set; }
        public double AddToQueueTime { get; set; }
        public double GetFromQueueTime { get; set; }
        public double FinishTime { get; set; }
        public Package()
        {
        }

        public Package(int id, int sourceId, int size, double time)
        {
            Id = id;
            Size = size;
            ComingTime = time;
            AddToQueueTime = 0;
            GetFromQueueTime = 0;
            FinishTime = 0;
            SourceId = sourceId;
        }
    }
}
