using System;
using System.Collections.Generic;
using System.Diagnostics;
using OAST.DemandAllocation.BruteForceTools;
using OAST.DemandAllocation.EvolutionAlgorithm;
using OAST.DemandAllocation.History;
using OAST.DemandAllocation.Output;

namespace OAST.DemandAllocation.BruteForceAlgorithm
{
    public class BruteForceAlgorithm : IBruteForceAlgorithm
    {
        public List<Chromosome> Population { get; set; }
        public string OutputFileName { get; set; }
        public int NumberOfIterations { get; set; }
        public Stopwatch Timer { get; set; }
        
        private readonly IBfTools _tools;
        private readonly IOutputSaver _outputSaver;
        private readonly IHistory _history;
        
        public BruteForceAlgorithm(IBfTools tools, 
            IOutputSaver outputSaver, 
            IHistory history)
        {
            _tools = tools;
            _outputSaver = outputSaver;
            _history = history;
            Population = new List<Chromosome>();
            OutputFileName = $"./outputs/bruteforce_output_{DateTime.UtcNow.ToString("yyyyMMddTHHmmss")}";
            NumberOfIterations = 0;
            Timer = new Stopwatch();
        }
        
        public void Run(Parameters parameters)
        {
            Timer.Start();
            
            int best = Int32.MaxValue;
            Chromosome bestChromosome = null!;
            var population = _tools.GenerateAllPossibleChromosomes();
            
            foreach (var chromosome in population)
            {
                chromosome.CalculateLinkLoads();
                var result = chromosome.CalculateMaxLoad();
                if (result < best)
                {
                    best = result;
                    bestChromosome = chromosome;
                    _history.AddChromosome(bestChromosome);
                }

                NumberOfIterations += 1;
            }
            
            Timer.Stop();
            _outputSaver.SaveResults(bestChromosome!, OutputFileName, parameters, NumberOfIterations, Timer.Elapsed);
        }
    }
}