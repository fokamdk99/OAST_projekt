using System.Collections.Generic;
using OAST.DemandAllocation.Demands;

namespace OAST.DemandAllocation.EvolutionAlgorithm
{
    public interface IChromosome
    {
        List<List<int>> PathLoads { get; set; }
        List<int> LinkLoads { get; set; }
        float SumOfLinkCosts { get; set; }
        int Rank { get; set; }
        int MaxLoad { get; set; }

        int CalculateMaxLoad();
        public int CalculateLinkLoads();
        List<int> GenerateGene(Demand demand);
    }
}