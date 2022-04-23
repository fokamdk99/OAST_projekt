using System.Collections.Generic;
using System.Linq;
using OAST.DemandAllocation.Demands;
using OAST.DemandAllocation.Fitness;
using OAST.DemandAllocation.Topology;

namespace OAST.DemandAllocation.EvolutionAlgorithm
{
    public class Chromosome : IChromosome
    {
        private readonly ITopology _topology;
        private readonly IFitnessFunction _fitnessFunction;
        
        public List<List<int>> PathLoads { get; set; }
        public List<int> LinkLoads { get; set; }
        public float SumOfLinkCosts { get; set; }
        public int Rank { get; set; }
        public int MaxLoad { get; set; }

        public Chromosome(ITopology topology, IFitnessFunction fitnessFunction, List<List<int>> pathLoads)
        {
            _topology = topology;
            _fitnessFunction = fitnessFunction;
            PathLoads = new List<List<int>>();
            LinkLoads = new List<int>();
            LinkLoads.AddRange(Enumerable.Repeat(0, _topology.Links.Count).ToList());
            PathLoads = pathLoads;
            _fitnessFunction = fitnessFunction;

            SumOfLinkCosts = 0;
            Rank = 0;
        }

        public int CalculateMaxLoad()
        {
            return _fitnessFunction.CalculateMaxLoad(this);
        }
        
        public int CalculateLinkLoads()
        {
            int load = 0;
            foreach (var link in _topology.Links.Select((item, i) => new {item, i}))
            {
                var linkLoad = CalculateLinkLoad(_topology.Demands, link.item.LinkId);
                LinkLoads[link.i] = linkLoad;

                load += linkLoad;
            }
            
            SumOfLinkCosts = load;
            return load;
        }

        private int CalculateLinkLoad(List<Demand> demands, int linkId)
        {
            int sum = 0;
            foreach (var demand in demands
                .Select((value, demandIndex) => new {value, demandIndex})) // dla kazdego demanda
            {
                foreach (var path in demand.value.DemandPaths
                    .Select((value, pathIndex) => new {value, pathIndex}))
                {
                    bool linkInPath = path.value.LinkIds.Contains(linkId);
                    if (!linkInPath)
                    {
                        continue;
                    }

                    sum += PathLoads.ElementAt(demand.demandIndex).ElementAt(path.pathIndex);
                }
            }

            return sum;
        }
    }
}