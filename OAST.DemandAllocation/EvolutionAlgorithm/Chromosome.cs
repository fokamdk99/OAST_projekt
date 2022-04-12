using System.Collections.Generic;
using System.Linq;
using OAST.DemandAllocation.Demands;

namespace OAST.DemandAllocation.EvolutionAlgorithm
{
    public class Chromosome : IChromosome
    {
        public Chromosome(List<Demand> demands)
        {
            PathCosts = new List<List<int>>();
            foreach (var demand in demands)
            {
                // dodaj gen do chromosomu
                PathCosts.Add(Enumerable.Repeat(0, demand.NumberOfDemandPaths).ToList());
            }
        }

        public List<List<int>> PathCosts { get; set; }
    }
}