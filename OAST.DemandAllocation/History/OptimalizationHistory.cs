using System.Collections.Generic;
using OAST.DemandAllocation.EvolutionAlgorithm;

namespace OAST.DemandAllocation.History
{
    public class OptimalizationHistory : IHistory
    {
        public List<Chromosome> BestChromosomes { get; set; }
        
        public OptimalizationHistory()
        {
            BestChromosomes = new List<Chromosome>();
        }

        public void AddChromosome(Chromosome chromosome)
        {
            BestChromosomes.Add(chromosome);
        }
    }
}