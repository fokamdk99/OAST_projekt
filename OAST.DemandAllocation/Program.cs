using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using OAST.DemandAllocation.BruteForceAlgorithm;
using OAST.DemandAllocation.Criteria;
using OAST.DemandAllocation.EvolutionAlgorithm;
using OAST.DemandAllocation.FileReader;

namespace OAST.DemandAllocation
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddDemandAllocationFeature()
                .BuildServiceProvider();

            Console.WriteLine($"number of parameters: {args.Length}");
            
            var fileReader = serviceProvider.GetRequiredService<IFileReader>();
            fileReader.FileName = "./files/net4.txt";
            fileReader.ReadFile();

            if (args.Length == 6)
            {
                var parameters = new Parameters
                {
                    Mi = Int32.Parse(args.ElementAt(1)),
                    CrossoverProbability = float.Parse(args.ElementAt(2)),
                    MutationProbability = float.Parse(args.ElementAt(3)),
                    Seed = Int32.Parse(args.ElementAt(4)),
                    StopCriteria = (StopCriteriaType) Int32.Parse(args.ElementAt(5))
                };

                switch (parameters.StopCriteria)
                {
                    
                    case StopCriteriaType.Time:
                        var timeAlgorithm = serviceProvider.GetRequiredService<IEvolutionAlgorithm<TimeCriteria>>();

                        var timeCriteria = new TimeCriteria(20);
                        timeAlgorithm.SetParams(timeCriteria);
                        timeAlgorithm.Run(parameters, EvaluateTimeCriteria.Evaluate, EvaluateTimeCriteria.StartTimer, EvaluateTimeCriteria.StopTimer);
                        return;
                    case StopCriteriaType.NumberOfGenerations:
                        var generationsAlgorithm = serviceProvider.GetRequiredService<IEvolutionAlgorithm<GenerationsCriteria>>();
                        
                        var generationsCriteria = new GenerationsCriteria(100);
                        generationsAlgorithm.SetParams(generationsCriteria);
                        generationsAlgorithm.Run(parameters, EvaluateGenerationsCriteria.Evaluate, null, null);
                        return;
                    case StopCriteriaType.NumberOfMutations:
                        var evolutionAlgorithm = serviceProvider.GetRequiredService<IEvolutionAlgorithm<MutationsCriteria>>();

                        var mutationsCriteria = new MutationsCriteria(50);
                        evolutionAlgorithm.SetParams(mutationsCriteria);
                        evolutionAlgorithm.Run(parameters, EvaluateMutationsCriteria.Evaluate, null, null);
                        return;
                    case StopCriteriaType.BestSolution:
                        var bestSolutionAlgorithm = serviceProvider.GetRequiredService<IEvolutionAlgorithm<BestSolutionCriteria>>();

                        var bestSolutionCriteria = new BestSolutionCriteria(10);
                        bestSolutionAlgorithm.SetParams(bestSolutionCriteria);
                        bestSolutionAlgorithm.Run(parameters, EvaluateBestSolutionCriteria.Evaluate, null, null);
                        return;
                    default:
                        Console.WriteLine("Invalid stop criteria type.");
                        return;
                }
            }

            if (args.Length == 1)
            {
                var bruteForceAlgorithm = serviceProvider.GetRequiredService<IBruteForceAlgorithm>();
                bruteForceAlgorithm.Run();
                return;
            }
            
            if (args.Length != 6 || args.Length != 1)
            {
                Console.WriteLine("Valid formats:\n" +
                                  "<algorithm> [<number items in initial population> <crossover probability> " +
                                  "<mutation probability> <seed> <stop criteria>]\n" +
                                  "brute force algorithm - 1\n" +
                                  "evolution algorithm - 2\n" +
                                  "stop criteria: time - 1, number of generations - 2, number of mutations - 3, best chromosome - 4");
            }
        }
    }
}