using System.Collections.Generic;

namespace OAST.DemandAllocation.Demands
{
    public interface IDemand
    {
        int DemandId { get; set; }
        int StartNodeId { get; set; }
        int EndNodeId { get; set; }
        int DemandVolume { get; set; }
        int NumberOfDemandPaths { get; set; }
        List<DemandPath> DemandPaths { get; set; }
    }
}