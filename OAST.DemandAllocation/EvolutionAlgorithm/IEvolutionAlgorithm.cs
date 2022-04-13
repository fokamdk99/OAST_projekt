using System.Collections.Generic;
using OAST.DemandAllocation.Links;

namespace OAST.DemandAllocation.EvolutionAlgorithm
{
    public interface IEvolutionAlgorithm
    {
        int Iteration { get; set; }
        List<Chromosome> Population { get; set; }
        int Mi { get; set; }
    }
}