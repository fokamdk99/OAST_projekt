using System.Collections.Generic;
using System.Linq;
using OAST.DemandAllocation.Demands;
using OAST.DemandAllocation.EvolutionAlgorithm;
using OAST.DemandAllocation.Fitness;
using OAST.DemandAllocation.Topology;

namespace OAST.DemandAllocation.BruteForceTools
{
    public class BfTools : IBfTools
    {
        private List<List<int>> Solutions { get; set; }

        private readonly ITopology _topology;
        private readonly IFitnessFunction _fitnessFunction;

        public BfTools(ITopology topology, 
            IFitnessFunction fitnessFunction)
        {
            _topology = topology;
            _fitnessFunction = fitnessFunction;
            Solutions = new List<List<int>>();
        }

        // generate all possible permutations of a given gene
        public List<List<int>> Recursion(List<int> vector, 
            int depth, 
            int bandwidth, 
            int length)
        {
            if (depth == bandwidth)
            {
                Solutions.Add(vector);
                return Solutions;
            }

            for (int i = length; i < vector.Count; i++)
            {
                var copy = new List<int>();
                foreach (var item in vector)
                {
                    copy.Add(item);
                }

                copy[i] += 1;
                
                Recursion(copy, depth + 1, bandwidth, i);
            }

            return Solutions;
        }
        
        // generate permutations of all genes
        public List<List<int>> GetPermutations(
            List<List<int>> listOfLists)
        {
            return listOfLists.Skip(1)
                .Aggregate(listOfLists.First()
                        .Select(c => new List<int>() { c }), // initial value dla czteroelementowych genow: lista{first[0]}, lista{first[1]}, lista{first[2]}, lista{first[3]} 
                    (previous, next) => previous
                        .SelectMany(p => next.Select(d => new List<int>(p) { d }))).ToList();
        }

        // generate chromosomes based on permutations of all genes
        public List<Chromosome> GenerateAllPossibleChromosomes()
        {
            var permutations = GenerateAllPossiblePermutations(_topology.Demands);
            List<Chromosome> population = new List<Chromosome>();
            List<List<int>> indicesPermutations = new List<List<int>>();
            foreach (var genePermutations in permutations)
            {
                List<int> indices = new List<int>();
                indices.AddRange(Enumerable.Range(0, genePermutations.Count));
                indicesPermutations.Add(indices);
            }

            var result = GetPermutations(indicesPermutations);
            foreach (var indicesSet in result)
            {
                var chromosome = new Chromosome(_topology, _fitnessFunction, SetPathLoads(permutations, indicesSet));
                population.Add(chromosome);
            }

            return population;
        }

        // create a list of all possible values of genes
        public List<List<List<int>>> GenerateAllPossiblePermutations(List<Demand> demands)
        {
            List<List<List<int>>> genePermutations = new List<List<List<int>>>();
            foreach (var demand in demands)
            {
                genePermutations.Add(GenerateGenePermutations(demand));
            }

            return genePermutations;
        }

        // create inital vector for gene permutations
        public List<List<int>> GenerateGenePermutations(Demand demand)
        {
            var numberOfDemandPaths = demand.NumberOfDemandPaths;
            var demandVolume = demand.DemandVolume;

            var vector = new List<int>();
            vector.AddRange(Enumerable.Repeat(0, numberOfDemandPaths).ToList());

            Solutions = new List<List<int>>();
            var permutations = Recursion(vector, 0, demandVolume, 0);

            return permutations;
        }
        
        public static List<List<int>> SetPathLoads(List<List<List<int>>> permutations, List<int> indicesSet)
        {
            List<List<int>> gene = new List<List<int>>();
            int demand = 0;
            foreach (var index in indicesSet)
            {
                gene.Add(permutations.ElementAt(demand).ElementAt(index));
                demand += 1;
            }

            return gene;
        }
    }
}