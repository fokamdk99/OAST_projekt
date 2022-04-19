using System.Collections.Generic;

namespace OAST.DemandAllocation.EvolutionAlgorithm
{
    public interface IEvolutionAlgorithm
    {
        int Iteration { get; set; }
        List<Chromosome> Population { get; set; }
        int Mi { get; set; }
        public int NumberOfIterations { get; set; }

        void Run();
    }
}