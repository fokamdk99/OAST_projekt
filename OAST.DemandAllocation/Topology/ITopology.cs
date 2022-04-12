using System.Collections.Generic;
using OAST.DemandAllocation.Demands;
using OAST.DemandAllocation.Links;

namespace OAST.DemandAllocation.Topology
{
    public interface ITopology
    {
        List<Link> Links { get; set; }
        List<Demand> Demands { get; set; }
    }
}