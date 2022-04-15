using System.Collections.Generic;
using OAST.DemandAllocation.EvolutionAlgorithm;

namespace OAST.DemandAllocation.EvolutionTools
{
    public interface ITools
    {
        float CrossoverProbability { get; set; }
        float MutationProbability { get; set; }

        List<Chromosome> PerformCrossovers(List<Chromosome> chromosomes);
        List<Chromosome> PerformMutations(List<Chromosome> chromosomes);
        int GenerateRandomIntNumber(int range);
        float GenerateRandomFloatNumber();
    }
}