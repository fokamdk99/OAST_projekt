using System.Collections.Generic;
using System.Linq;
using OAST.DemandAllocation.Demands;
using OAST.DemandAllocation.Links;
using OAST.DemandAllocation.Topology;

namespace OAST.DemandAllocation.EvolutionAlgorithm
{
    public class EvolutionAlgorithm: IEvolutionAlgorithm
    {
        public int Iteration { get; set; }
        public List<int> LinkLoads { get; set; }
        public List<IChromosome> Population { get; set; }
        public int Mi { get; set; }
        private readonly ITopology _topology;
        
        public EvolutionAlgorithm(ITopology topology, int mi)
        {
            _topology = topology;
            Iteration = 0;
            LinkLoads = new List<int>();
            LinkLoads.AddRange(Enumerable.Repeat(0, _topology.Links.Count).ToList());
            Population = new List<IChromosome>();
            Mi = mi;
            for (int i = 0; i < Mi; i++)
            {
                Population.Add(new Chromosome(_topology.Demands));
            }
        }
        public void Calculate()
        {
            
            float sumOfLinkCosts = Evaluate(_topology.Links, _topology.Demands);
        }

        public float Evaluate(List<Link> links, List<Demand> demands)
        {
            float cost = 0;
            foreach (var link in links.Select((item, i) => new {item, i}))
            {
                int linkCost = CalculateLinkLoad(demands, link.item.LinkId);
                LinkLoads[link.i] = linkCost;
                
                cost += linkCost;
            }

            return cost;
        }

        public int CalculateLinkLoad(List<Demand> demands, int linkId)
        {
            int sum = 0;
            foreach (var demand in demands) // dla kazdego demanda
            {
                foreach (var path in demand.DemandPaths)
                {
                    bool linkInPath = path.LinkIds.Contains(linkId);
                    if (!linkInPath)
                    {
                        continue;
                    }

                    sum += path.Load;
                }
            }

            return sum;
        }

        public void TournamentSelection()
        {
            
        }
    }
}