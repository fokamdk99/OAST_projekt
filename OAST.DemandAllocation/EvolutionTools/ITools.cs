using System.Collections.Generic;
using OAST.DemandAllocation.EvolutionAlgorithm;

namespace OAST.DemandAllocation.EvolutionTools
{
    public interface ITools
    {
        float CrossoverProbability { get; set; }
        float MutationProbability { get; set; }

        List<Chromosome> PerformCrossovers(List<Chromosome> chromosomes);
        Chromosome? PerformCrossover(Chromosome x, Chromosome y);
        List<Chromosome> PerformMutations(List<Chromosome> chromosomes);
        Chromosome? PerformMutation(Chromosome chromosome);
        int GenerateRandomIntNumber(int range);
        float GenerateRandomFloatNumber();
    }
}