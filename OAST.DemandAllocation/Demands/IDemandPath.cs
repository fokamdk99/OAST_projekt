using System.Collections.Generic;

namespace OAST.DemandAllocation.Demands
{
    public interface IDemandPath
    {
        int DemandPathId { get; set; }
        List<int> LinkIds { get; set; }
        int Load { get; set; }
    }
}