using System.Collections.Generic;
using OAST.DemandAllocation.Links;

namespace OAST.DemandAllocation.EvolutionAlgorithm
{
    public interface IReproduction
    {
        List<Chromosome> SelectReproductionSet(List<Chromosome> population);
        void CalculateRanks(List<Chromosome> population, List<Link> links);
    }
}