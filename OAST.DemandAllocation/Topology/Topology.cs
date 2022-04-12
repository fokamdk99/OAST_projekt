using System.Collections.Generic;
using OAST.DemandAllocation.Demands;
using OAST.DemandAllocation.Links;

namespace OAST.DemandAllocation.Topology
{
    public class Topology : ITopology
    {
        public List<Link> Links { get; set; }
        public List<Demand> Demands { get; set; }

        public Topology()
        {
            Links = new List<Link>();
            Demands = new List<Demand>();
        }
    }
}