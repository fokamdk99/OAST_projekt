using System.Collections.Generic;
using OAST.DemandAllocation.Demands;
using OAST.DemandAllocation.Links;

namespace OAST.DemandAllocation.EvolutionAlgorithm
{
    public interface IChromosome
    {
        List<List<int>> PathLoads { get; set; }
        List<int> LinkLoads { get; set; }
        float SumOfLinkCosts { get; set; }
        float Rank { get; set; }
        int MaxLoad { get; set; }

        int CalculateMaxLoad(List<Link> links);
        public int CalculateLinkLoads(List<Demand> demands, List<Link> links);
    }
}