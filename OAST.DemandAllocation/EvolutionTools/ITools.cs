using System.Collections.Generic;
using OAST.DemandAllocation.Demands;
using OAST.DemandAllocation.EvolutionAlgorithm;

namespace OAST.DemandAllocation.EvolutionTools
{
    public interface ITools
    {
        float CrossoverProbability { get; set; }
        float MutationProbability { get; set; }

        List<Chromosome> PerformCrossovers(List<Chromosome> chromosomes);
        Chromosome? PerformCrossover(Chromosome x, Chromosome y);
        List<Chromosome> PerformMutations<T>(List<Chromosome> chromosomes, T stopCriteria);
        Chromosome? PerformMutation<T>(Chromosome chromosome, T stopCriteria);
        List<int> GenerateGene(Demand demand);
        List<List<int>> SetPathLoads();
        void SetParameters(float crossoverProbability, float mutationProbability, int seed);
    }
}