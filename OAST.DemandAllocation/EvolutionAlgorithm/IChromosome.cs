using System.Collections.Generic;

namespace OAST.DemandAllocation.EvolutionAlgorithm
{
    public interface IChromosome
    {
        List<List<int>> PathCosts { get; set; }
    }
}