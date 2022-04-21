using System;
using System.Collections.Generic;
using OAST.DemandAllocation.BruteForceTools;
using OAST.DemandAllocation.EvolutionAlgorithm;
using OAST.DemandAllocation.Output;

namespace OAST.DemandAllocation.BruteForceAlgorithm
{
    public class BruteForceAlgorithm : IBruteForceAlgorithm
    {
        public List<Chromosome> Population { get; set; }
        
        private readonly IBfTools _tools;
        private readonly IOutputSaver _outputSaver;
     
        public BruteForceAlgorithm(IBfTools tools, 
            IOutputSaver outputSaver)
        {
            _tools = tools;
            _outputSaver = outputSaver;
            Population = new List<Chromosome>();
            
        }
        
        public void Run(string outputFileName)
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
            
            _outputSaver.SaveResults(bestChromosome!, outputFileName);
        }
    }
}