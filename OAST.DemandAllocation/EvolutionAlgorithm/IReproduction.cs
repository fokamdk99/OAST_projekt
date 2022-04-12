using System.Collections.Generic;

namespace OAST.DemandAllocation.EvolutionAlgorithm
{
    public interface IReproduction
    {
        List<IChromosome> SelectReproductionSet();
    }
}