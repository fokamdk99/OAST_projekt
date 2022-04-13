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
        public List<Chromosome> Population { get; set; }
        public int Mi { get; set; }
        private readonly ITopology _topology;
        
        public EvolutionAlgorithm(ITopology topology, int mi)
        {
            _topology = topology;
            Iteration = 0;
            Population = new List<Chromosome>();
            Mi = mi;
            for (int i = 0; i < Mi; i++)
            {
                // mi razy inicjalizuj tablice
                Population.Add(new Chromosome(_topology.Demands, _topology.Links));
            }
        }
        public void Calculate()
        {
            foreach (var chromosome in Population)
            {
                float sumOfLinkCosts = Evaluate(_topology.Links, _topology.Demands, chromosome);
                chromosome.SumOfLinkCosts = sumOfLinkCosts;
            }
            
        }

        public float Evaluate(List<Link> links, List<Demand> demands, IChromosome chromosome)
        {
            float cost = 0;
            foreach (var link in links.Select((item, i) => new {item, i}))
            {
                var linkCost = chromosome.CalculateLinkLoads(demands, links);

                cost += linkCost;
            }

            return cost;
        }
    }
}