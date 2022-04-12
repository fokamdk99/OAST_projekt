namespace OAST.DemandAllocation.Links
{
    public interface ILink
    {
        int LinkId { get; set; }
        int StartNodeId { get; set; }
        int EndNodeId { get; set; }
        int NumberOfFibrePairs { get; set; }
        float FibrePairCost { get; set; }
        int NumberOfLambdasInFibre { get; set; }
    }
}