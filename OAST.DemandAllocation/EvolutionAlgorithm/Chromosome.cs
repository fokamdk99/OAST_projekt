using System.Collections.Generic;
using System.Linq;
using OAST.DemandAllocation.Demands;
using OAST.DemandAllocation.Topology;

namespace OAST.DemandAllocation.EvolutionAlgorithm
{
    public class Chromosome : IChromosome
    {
        private readonly ITopology _topology;
        
        public List<List<int>> PathLoads { get; set; }
        public List<int> LinkLoads { get; set; }
        public float SumOfLinkCosts { get; set; }
        public float Rank { get; set; }
        public int MaxLoad { get; set; }

        public Chromosome(ITopology topology)
        {
            _topology = topology;
            PathLoads = new List<List<int>>();
            LinkLoads = new List<int>();
            LinkLoads.AddRange(Enumerable.Repeat(0, _topology.Links.Count).ToList());
            foreach (var demand in _topology.Demands)
            {
                // dodaj gen do chromosomu
                PathLoads.Add(Enumerable.Repeat(0, demand.NumberOfDemandPaths).ToList());
            }

            SumOfLinkCosts = 0;
            Rank = 0;
        }
        
        public int CalculateMaxLoad()
        {
            List<int> linkLoads = new List<int>();
            foreach (var linkLoad in LinkLoads
                .Select((value, linkIndex) => new {value, linkIndex}))
            {
                linkLoads.Add(linkLoad.value - _topology.Links.ElementAt(linkLoad.linkIndex).LinkCapacity); // maximum (over all links)
            }

            var result = linkLoads.Max();
            MaxLoad = result;

            return result;
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
        
        public float EvaluateLinkLoads()
        {
            float load = 0;
            foreach (var link in _topology.Links.Select((item, i) => new {item, i}))
            {
                var linkLoad = CalculateLinkLoads();

                load += linkLoad;
            }

            SumOfLinkCosts = load;
            return load;
        }
    }
}