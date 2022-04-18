using System.Collections.Generic;
using OAST.DemandAllocation.EvolutionTools;
using OAST.DemandAllocation.Topology;

namespace OAST.DemandAllocation.EvolutionAlgorithm
{
    public class EvolutionAlgorithm: IEvolutionAlgorithm
    {
        public int Iteration { get; set; }
        public int NumberOfIterations { get; set; }
        public List<Chromosome> Population { get; set; }
        public int Mi { get; set; }
        
        private readonly ITopology _topology;
        private readonly IReproduction _reproduction;
        private readonly IInheritance _inheritance;
        private readonly ITools _tools;
        
        public EvolutionAlgorithm(ITopology topology, 
            IReproduction reproduction,
            ITools tools,
            IInheritance inheritance,
            int mi)
        {
            _topology = topology;
            Iteration = 0;
            Population = new List<Chromosome>();
            Mi = mi;
            _inheritance = inheritance;
            _tools = tools;
            _reproduction = reproduction;
            for (int i = 0; i < Mi; i++)
            {
                // mi razy inicjalizuj tablice
                Population.Add(new Chromosome(_topology));
            }

            NumberOfIterations = 100;
        }
        public void Run()
        {
            foreach (var chromosome in Population)
            {
                chromosome.CalculateLinkLoads();
            }

            while (Iteration < NumberOfIterations)
            {
                var reproductionSet = _reproduction.SelectReproductionSet(Population);
                var chromosomesWithCrossovers = _tools.PerformCrossovers(reproductionSet);
                var chromosomesWithMutations = _tools.PerformMutations(chromosomesWithCrossovers);
                Population = _inheritance.SelectInheritanceSet(chromosomesWithMutations, Population);
                Iteration += 1;
            }
        }
    }
}