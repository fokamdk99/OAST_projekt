using System;
using System.Collections.Generic;
using System.Linq;
using OAST.DemandAllocation.EvolutionAlgorithm;
using OAST.DemandAllocation.Topology;

namespace OAST.DemandAllocation.EvolutionTools
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

        public List<Chromosome> PerformCrossovers(List<Chromosome> chromosomes)
        {
            List<Tuple<Chromosome, Chromosome>> chromosomePairs = new List<Tuple<Chromosome, Chromosome>>();
            var chromosomeIndices = GenerateWithoutDuplicates(chromosomes.Count);
            for (int i = 0; i < chromosomeIndices.Count / 2; i++)
            {
                Tuple<Chromosome, Chromosome> chromosomePair = 
                    new Tuple<Chromosome, Chromosome>(
                        chromosomes.ElementAt(chromosomeIndices.ElementAt(i)), 
                        chromosomes.ElementAt(chromosomeIndices.ElementAt(i+1)));
                
                chromosomePairs.Add(chromosomePair);
            }

            foreach (var pair in chromosomePairs)
            {
                var crossover = PerformCrossover(pair.Item1, pair.Item2);
                if (crossover != null)
                {
                    chromosomes.Add(crossover);
                }
            }

            return chromosomes;
        }

        private Chromosome? PerformCrossover(Chromosome x, Chromosome y)
        {
            var probability = GenerateRandomFloatNumber();
            if (CrossoverProbability > probability)
            {
                return null;
            }
            
            Chromosome crossover = new Chromosome(_topology);
            foreach (var demand in _topology.Demands.Select((value, i) => new {value, i}))
            {
                var parent = GenerateRandomFloatNumber();
                crossover.PathLoads[demand.i] =
                    parent <= 0.5 ? x.PathLoads.ElementAt(demand.i) : y.PathLoads.ElementAt(demand.i);
            }

            return crossover;
        }

        public List<Chromosome> PerformMutations(List<Chromosome> chromosomes)
        {
            List<Chromosome> chromosomesWithMutations = new List<Chromosome>();
            foreach (var chromosome in chromosomes)
            {
                var mutation = PerformMutation(chromosome);
                chromosomesWithMutations.Add(mutation ?? chromosome);
            }

            return chromosomesWithMutations;
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

        private List<int> GenerateWithoutDuplicates(int range)
        {
            List<int> possible = Enumerable.Range(0, range).ToList();
            List<int> listNumbers = new List<int>();
            for (int i = 0; i < range; i++)
            {
                Random rnd = new Random();
                int index = rnd.Next(0, possible.Count);
                listNumbers.Add(possible[index]);
                possible.RemoveAt(index);
            }

            return listNumbers;
        }
    }
}