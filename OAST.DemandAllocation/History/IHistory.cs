using System.Collections.Generic;
using OAST.DemandAllocation.EvolutionAlgorithm;

namespace OAST.DemandAllocation.History
{
    public interface IHistory
    {
        List<Chromosome> BestChromosomes { get; set; }
        void AddChromosome(Chromosome chromosome);
    }
}