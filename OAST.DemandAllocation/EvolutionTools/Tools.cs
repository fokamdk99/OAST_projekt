using System;
using System.Collections.Generic;
using System.Linq;
using OAST.DemandAllocation.Criteria;
using OAST.DemandAllocation.Demands;
using OAST.DemandAllocation.EvolutionAlgorithm;
using OAST.DemandAllocation.Fitness;
using OAST.DemandAllocation.RandomNumberGenerator;
using OAST.DemandAllocation.Topology;

namespace OAST.DemandAllocation.EvolutionTools
{
    public class Tools : ITools
    {
        public float CrossoverProbability { get; set; }
        public float MutationProbability { get; set; }
        public int Seed { get; set; }

        private readonly ITopology _topology;
        private readonly IFitnessFunction _fitnessFunction;
        private readonly IRandomNumberGenerator _randomNumberGenerator;
        private  Random _random;

        public Tools(ITopology topology, 
            IRandomNumberGenerator randomNumberGenerator, 
            IFitnessFunction fitnessFunction)
        {
            _topology = topology;
            _randomNumberGenerator = randomNumberGenerator;
            _fitnessFunction = fitnessFunction;
            CrossoverProbability = 0.7f;
            MutationProbability = 0.3f;
            Seed = 24699;
            _random = new Random(Seed);
        }

        public void SetParameters(float crossoverProbability, float mutationProbability, int seed)
        {
            CrossoverProbability = crossoverProbability;
            MutationProbability = mutationProbability;
            Seed = seed;
            _random = new Random(Seed);
        }

        public List<Chromosome> PerformCrossovers(List<Chromosome> chromosomes)
        {
            List<Chromosome> crossovers = new List<Chromosome>();
            while (crossovers.Count < chromosomes.Count)
            {
                int mother = _randomNumberGenerator.GenerateRandomIntNumber(chromosomes.Count);
                int father;
                do
                {
                    father = _randomNumberGenerator.GenerateRandomIntNumber(chromosomes.Count);
                } while (mother == father);
                
                var crossover = PerformCrossover(chromosomes.ElementAt(mother), chromosomes.ElementAt(father));
                if (crossover != null)
                {
                    crossovers.Add(crossover);
                }
            }

            return crossovers;
        }

        public Chromosome? PerformCrossover(Chromosome x, Chromosome y)
        {
            var probability = _randomNumberGenerator.GenerateRandomFloatNumber();
            if (CrossoverProbability < probability)
            {
                return null;
            }
            
            Chromosome crossover = new Chromosome(_topology, _fitnessFunction, SetPathLoads());
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
            while (bandwidth != 0)
            {
                var pathIndex = _random.Next(0, pathLoads.Count);
                pathLoads[pathIndex] += 1;
                bandwidth -= 1;
            }

            return pathLoads.OrderBy(a => _random.Next()).ToList();
        }
    }
}