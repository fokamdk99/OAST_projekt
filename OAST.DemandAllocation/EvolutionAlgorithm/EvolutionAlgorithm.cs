using System;
using System.Collections.Generic;
using System.Linq;
using OAST.DemandAllocation.Criteria;
using OAST.DemandAllocation.EvolutionTools;
using OAST.DemandAllocation.Fitness;
using OAST.DemandAllocation.History;
using OAST.DemandAllocation.Output;
using OAST.DemandAllocation.Topology;

namespace OAST.DemandAllocation.EvolutionAlgorithm
{
    public class EvolutionAlgorithm<T>: IEvolutionAlgorithm<T> where T : class
    {
        public List<Chromosome> Population { get; set; }
        public int Mi { get; set; }
        public T StopParameters { get; set; }
        public string OutputFileName { get; set; }
        
        private readonly IReproduction _reproduction;
        private readonly IInheritance _inheritance;
        private readonly ITools _tools;
        private readonly IHistory _history;
        private readonly IOutputSaver _outputSaver;
        
        public EvolutionAlgorithm(ITopology topology, 
            IReproduction reproduction,
            ITools tools,
            IInheritance inheritance, 
            IFitnessFunction fitnessFunction, 
            IHistory history, 
            IOutputSaver outputSaver)
        {
            Population = new List<Chromosome>();
            Mi = 10;
            _inheritance = inheritance;
            _history = history;
            _outputSaver = outputSaver;
            _tools = tools;
            _reproduction = reproduction;
            for (int i = 0; i < Mi; i++)
            {
                // mi razy inicjalizuj tablice
                Population.Add(new Chromosome(topology, fitnessFunction, _tools.SetPathLoads()));
            }
            OutputFileName = OutputFileName = $"./outputs/evolution_output_{DateTime.UtcNow.ToString("yyyyMMddTHHmmss")}.txt";
        }

        public void SetParams(T parameters)
        {
            StopParameters = parameters;
        }
        public void Run(Parameters parameters, Func<T, bool> stopCriteria, Action<T>? setupStopCriteria, Action<T>? disposeStopCriteria)
        {
            Mi = parameters.Mi;
            _tools.SetParameters(parameters.CrossoverProbability, parameters.MutationProbability, parameters.Seed);
            _reproduction.SetParameters(parameters.Seed);
            
            foreach (var chromosome in Population)
            {
                chromosome.CalculateLinkLoads();
            }

            if (setupStopCriteria != null)
            {
                setupStopCriteria(StopParameters);
            }
            
            while (!stopCriteria(StopParameters))
            {
                var reproductionSet = _reproduction.SelectReproductionSet(Population);
                var chromosomesWithCrossovers = _tools.PerformCrossovers(reproductionSet);
                var chromosomesWithMutations = _tools.PerformMutations<T>(chromosomesWithCrossovers, StopParameters);
                Population = _inheritance.SelectInheritanceSet(chromosomesWithMutations, Population);
                _history.AddChromosome(Population.ElementAt(0));

                HandleStopParameters();
            }

            if (disposeStopCriteria != null)
            {
                disposeStopCriteria(StopParameters);
            }
            
            _outputSaver.SaveResults(Population.ElementAt(0), OutputFileName, parameters);
        }
        
        public void HandleStopParameters()
        {
            
            if (typeof(T) == typeof(GenerationsCriteria))
            {
                var generationsCriteria = StopParameters as GenerationsCriteria;
                generationsCriteria!.CurrentGeneration += 1;
                return;
            }

            if (typeof(T) == typeof(TimeCriteria))
            {
                var timeCriteria = StopParameters as TimeCriteria;
                timeCriteria!.ElapsedTime = timeCriteria.Timer.Elapsed;
            }

            if (typeof(T) == typeof(BestSolutionCriteria))
            {
                var bestSolutionCriteria = StopParameters as BestSolutionCriteria;
                var generationBestSolution = Population.ElementAt(0).MaxLoad;
                if (generationBestSolution < bestSolutionCriteria!.CurrentBestSolution)
                {
                    bestSolutionCriteria!.CurrentBestSolution = generationBestSolution;
                    bestSolutionCriteria!.NumberOfGenerationsWithoutBetterSolution = 0;
                }
                else
                {
                    bestSolutionCriteria!.NumberOfGenerationsWithoutBetterSolution += 1;
                }
                
                bestSolutionCriteria!.BestSolutions
                    .Add(bestSolutionCriteria!.CurrentBestSolution);
                bestSolutionCriteria!.GenerationsWithoutBetterSolution
                    .Add(bestSolutionCriteria!.NumberOfGenerationsWithoutBetterSolution);
            }
        }
    }
}