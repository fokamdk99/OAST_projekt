using System.Collections.Generic;

namespace OAST.DemandAllocation.EvolutionAlgorithm
{
    public interface IEvolutionAlgorithm
    {
        List<int> LinkLoads { get; set; }
        int Iteration { get; set; }
        List<IChromosome> Population { get; set; }
        int Mi { get; set; }
    }
}