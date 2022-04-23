using OAST.DemandAllocation.EvolutionAlgorithm;

namespace OAST.DemandAllocation.Fitness
{
    public interface IFitnessFunction
    {
        int CalculateMaxLoad(Chromosome chromosome);
    }
}