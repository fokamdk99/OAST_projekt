using System;
using System.Collections.Generic;
using System.Linq;
using OAST.DemandAllocation.Links;
using OAST.DemandAllocation.Tools;

namespace OAST.DemandAllocation.EvolutionAlgorithm
{
    public class Reproduction : IReproduction
    {
        private readonly ITools _tools;

        public Reproduction(ITools tools)
        {
            _tools = tools;
        }

        public void CalculateRanks(List<Chromosome> population, List<Link> links)
        {
            foreach (var chromosome in population)
            {
                chromosome.CalculateMaxLoad(links);
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
            int reproductionSetSize = population.Count / 2; // TODO: zmien pozniej rozmiar setu na lambda
            List<Chromosome> temporaryPopulation = new List<Chromosome>();
            foreach (var i in Enumerable.Range(0, reproductionSetSize))
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