using System;
using System.Collections.Generic;
using System.Linq;
using OAST.DemandAllocation.Criteria;
using OAST.DemandAllocation.Demands;
using OAST.DemandAllocation.EvolutionAlgorithm;
using OAST.DemandAllocation.RandomNumberGenerator;
using OAST.DemandAllocation.Topology;

namespace OAST.DemandAllocation.EvolutionTools
{
    public class Tools : ITools
    {
        public float CrossoverProbability { get; set; }
        public float MutationProbability { get; set; }

        private readonly ITopology _topology;
        private readonly IRandomNumberGenerator _randomNumberGenerator;
        
        

        public Tools(ITopology topology, 
            IRandomNumberGenerator randomNumberGenerator)
        {
            _topology = topology;
            _randomNumberGenerator = randomNumberGenerator;
            CrossoverProbability = 0.7f;
            MutationProbability = 0.3f;
        }

        public void SetParameters(float crossoverProbability, float mutationProbability, int seed)
        {
            CrossoverProbability = crossoverProbability;
            MutationProbability = mutationProbability;
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

        public Chromosome? PerformCrossover(Chromosome x, Chromosome y)
        {
            var probability = _randomNumberGenerator.GenerateRandomFloatNumber();
            if (CrossoverProbability < probability)
            {
                return null;
            }
            
            Chromosome crossover = new Chromosome(_topology, SetPathLoads());
            foreach (var demand in _topology.Demands.Select((value, i) => new {value, i}))
            {
                var parent = _randomNumberGenerator.GenerateRandomFloatNumber();
                crossover.PathLoads[demand.i] =
                    parent <= 0.5 ? x.PathLoads.ElementAt(demand.i) : y.PathLoads.ElementAt(demand.i);
            }

            crossover.CalculateLinkLoads();

            return crossover;
        }

        public List<Chromosome> PerformMutations<T>(List<Chromosome> chromosomes, T stopCriteria)
        {
            List<Chromosome> chromosomesWithMutations = new List<Chromosome>();
            foreach (var chromosome in chromosomes)
            {
                var mutation = PerformMutation(chromosome, stopCriteria);
                chromosomesWithMutations.Add(mutation ?? chromosome);
            }

            return chromosomesWithMutations;
        }

        public Chromosome? PerformMutation<T>(Chromosome chromosome, T stopCriteria)
        {
            var probability = _randomNumberGenerator.GenerateRandomFloatNumber();
            if (MutationProbability > probability)
            {
                return null;
            }

            if (typeof(T) == typeof(MutationsCriteria))
            {
                var criteria = stopCriteria as MutationsCriteria;
                if (criteria!.CurrentMutation == criteria!.NumberOfMutations)
                {
                    return null;
                }
                criteria!.CurrentMutation += 1;
            }

            var geneNumber = _randomNumberGenerator.GenerateRandomIntNumber(_topology.Demands.Count);
            var gene = chromosome.PathLoads.ElementAt(geneNumber);
            var numberOfPaths = gene.Count;
            var sourcePath = _randomNumberGenerator.GenerateRandomIntNumber(numberOfPaths);
            var destinationPath = _randomNumberGenerator.GenerateRandomIntNumber(numberOfPaths);
            var sourcePathValue = gene.ElementAt(sourcePath);
            gene[sourcePath] = gene.ElementAt(destinationPath);
            gene[destinationPath] = sourcePathValue;

            chromosome.CalculateLinkLoads();
            
            return chromosome;
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
        
        public List<List<int>> SetPathLoads()
        {
            List<List<int>> pathLoads = new List<List<int>>();
            foreach (var demand in _topology.Demands)
            {
                // dodaj gen do chromosomu
                pathLoads.Add(GenerateGene(demand));
            }

            return pathLoads;
        }
        
        public List<int> GenerateGene(Demand demand)
        {
            List<int> pathLoads = new List<int>();
            pathLoads.AddRange(Enumerable.Repeat<int>(0, demand.NumberOfDemandPaths));
            var bandwidth = demand.DemandVolume;
            Random rnd = new Random();
            while (bandwidth != 0)
            {
                var pathIndex = rnd.Next(0, pathLoads.Count);
                pathLoads[pathIndex] += 1;
                bandwidth -= 1;
            }

            return pathLoads.OrderBy(a => rnd.Next()).ToList();
        }
    }
}