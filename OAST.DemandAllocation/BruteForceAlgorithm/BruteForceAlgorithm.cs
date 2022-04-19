using System;
using System.Collections.Generic;
using OAST.DemandAllocation.BruteForceTools;
using OAST.DemandAllocation.EvolutionAlgorithm;
using OAST.DemandAllocation.EvolutionTools;
using OAST.DemandAllocation.Topology;

namespace OAST.DemandAllocation.BruteForceAlgorithm
{
    public class BruteForceAlgorithm : IBruteForceAlgorithm
    {
        public List<Chromosome> Population { get; set; }
        
        private readonly ITopology _topology;
        private readonly IReproduction _reproduction;
        private readonly IInheritance _inheritance;
        private readonly IBfTools _tools;
     
        public BruteForceAlgorithm(ITopology topology, 
            IReproduction reproduction, 
            IInheritance inheritance, 
            IBfTools tools)
        {
            _topology = topology;
            _reproduction = reproduction;
            _inheritance = inheritance;
            _tools = tools;
            Population = new List<Chromosome>();
            
        }
        
        public void Run()
        {
            int best = Int32.MaxValue;
            Chromosome bestChromosome = null;
            var population = _tools.GenerateAllPossibleChromosomes();
            foreach (var chromosome in population)
            {
                chromosome.CalculateLinkLoads();
                var result = chromosome.CalculateMaxLoad();
                if (result < best)
                {
                    best = result;
                    bestChromosome = chromosome;
                }
            }
        }
    }
}