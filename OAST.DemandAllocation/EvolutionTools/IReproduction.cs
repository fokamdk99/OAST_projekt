using System.Collections.Generic;
using OAST.DemandAllocation.EvolutionAlgorithm;

namespace OAST.DemandAllocation.EvolutionTools
{
    public interface IReproduction
    {
        List<Chromosome> SelectReproductionSet(List<Chromosome> population);
        void CalculateRanks(List<Chromosome> population);
        void SetParameters(int seed);
    }
}