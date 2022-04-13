using OAST.DemandAllocation.EvolutionAlgorithm;

namespace OAST.DemandAllocation.Tools
{
    public interface ITools
    {
        float CrossoverProbability { get; set; }
        float MutationProbability { get; set; }
        
        Chromosome? PerformCrossover(Chromosome x, Chromosome y);
        Chromosome? PerformMutation(Chromosome chromosome);
        int GenerateRandomIntNumber(int range);
        float GenerateRandomFloatNumber();
    }
}