using System.Collections.Generic;
using System.Linq;
using OAST.DemandAllocation.EvolutionAlgorithm;
using OAST.DemandAllocation.Links;
using OAST.DemandAllocation.Topology;

namespace OAST.DemandAllocation.EvolutionTools
{
    public class Reproduction : IReproduction
    {
        private readonly ITools _tools;
        private readonly ITopology _topology;

        public Reproduction(ITools tools, 
            ITopology topology)
        {
            _tools = tools;
            _topology = topology;
        }

        public void CalculateRanks(List<Chromosome> population, List<Link> links)
        {
            foreach (var chromosome in population)
            {
                chromosome.CalculateMaxLoad();
            }
            
            population.Sort(new ChromosomeComparer());


            int rank = 1;
            foreach (var chromosome in population)
            {
                chromosome.Rank = rank;
                rank += 1;
            }
        }
        
        public List<Chromosome> SelectReproductionSet(List<Chromosome> population)
        {
            CalculateRanks(population, _topology.Links);
            
            int reproductionSetSize = population.Count;
            List<Chromosome> temporaryPopulation = new List<Chromosome>();
            foreach (var i in Enumerable.Range(0, reproductionSetSize - 1))
            {
                int player = _tools.GenerateRandomIntNumber(population.Count);
                int opponent = _tools.GenerateRandomIntNumber(population.Count);
                temporaryPopulation.Add(population.ElementAt(player).Rank < population.ElementAt(opponent).Rank
                    ? population.ElementAt(player)
                    : population.ElementAt(opponent));
            }
            
            return temporaryPopulation;
        }
    }
}