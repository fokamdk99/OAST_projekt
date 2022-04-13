using System;
using System.Linq;
using OAST.DemandAllocation.EvolutionAlgorithm;
using OAST.DemandAllocation.Topology;

namespace OAST.DemandAllocation.Tools
{
    public class Tools : ITools
    {
        public float CrossoverProbability { get; set; }
        public float MutationProbability { get; set; }
        
        private readonly ITopology _topology;
        

        public Tools(ITopology topology)
        {
            _topology = topology;
        }

        public Chromosome? PerformCrossover(Chromosome x, Chromosome y)
        {
            var probability = GenerateRandomFloatNumber();
            if (CrossoverProbability > probability)
            {
                return null;
            }
            
            Chromosome crossover = new Chromosome(_topology.Demands, _topology.Links);
            foreach (var demand in _topology.Demands.Select((value, i) => new {value, i}))
            {
                var parent = GenerateRandomFloatNumber();
                crossover.PathLoads[demand.i] =
                    parent <= 0.5 ? x.PathLoads.ElementAt(demand.i) : y.PathLoads.ElementAt(demand.i);
            }

            return crossover;
        }

        public Chromosome? PerformMutation(Chromosome chromosome)
        {
            var probability = GenerateRandomFloatNumber();
            if (MutationProbability > probability)
            {
                return null;
            }

            var geneNumber = GenerateRandomIntNumber(_topology.Demands.Count);
            var gene = chromosome.PathLoads.ElementAt(geneNumber);
            var numberOfPaths = gene.Count;
            var sourcePath = GenerateRandomIntNumber(numberOfPaths);
            var destinationPath = GenerateRandomIntNumber(numberOfPaths);
            var sourcePathValue = gene.ElementAt(sourcePath);
            gene[sourcePath] = gene.ElementAt(destinationPath);
            gene[destinationPath] = sourcePathValue;

            return chromosome;
        }
        
        public int GenerateRandomIntNumber(int range)
        {
            Random rnd = new Random(24698);
            var number = rnd.Next(0, range);

            return number;
        }

        public float GenerateRandomFloatNumber()
        {
            Random rnd = new Random(24698);
            var number = (float)rnd.NextDouble();

            return number;
        }
    }
}