using System.Collections.Generic;
using OAST.DemandAllocation.EvolutionAlgorithm;

namespace OAST.DemandAllocation.EvolutionTools
{
    public interface IInheritance
    {
        int Eta { get; set; }

        List<Chromosome> SelectInheritanceSet(List<Chromosome> temporaryPopulation,
            List<Chromosome> currentPopulation);
    }
}