namespace OAST.DemandAllocation.FileReader
{
    public interface IFileReader
    {
        string Separator { get; set; }
        int NumberOfLinks { get; set; }
        int NumberOfDemands { get; set; }
        string FileName { get; set; }

        void ReadFile();

    }
}