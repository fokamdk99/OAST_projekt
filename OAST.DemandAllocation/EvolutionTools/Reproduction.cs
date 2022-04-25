using System.Collections.Generic;
using System.Linq;
using OAST.DemandAllocation.EvolutionAlgorithm;
using OAST.DemandAllocation.RandomNumberGenerator;

namespace OAST.DemandAllocation.EvolutionTools
{
    public class Reproduction : IReproduction
    {
        private readonly IRandomNumberGenerator _randomNumberGenerator;

        public Reproduction(IRandomNumberGenerator randomNumberGenerator)
        {
            _randomNumberGenerator = randomNumberGenerator;
        }

        public void SetParameters(int seed)
        {
            _randomNumberGenerator.SetParameters(seed);
        }

        public void CalculateRanks(List<Chromosome> population)
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
            CalculateRanks(population);
            
            int reproductionSetSize = population.Count;
            List<Chromosome> temporaryPopulation = new List<Chromosome>();
            foreach (var i in Enumerable.Range(0, reproductionSetSize))
            {
                int player = _randomNumberGenerator.GenerateRandomIntNumber(population.Count);
                int opponent;
                do
                {
                    opponent = _randomNumberGenerator.GenerateRandomIntNumber(population.Count);
                } while (opponent == player);
                temporaryPopulation.Add(population.ElementAt(player).Rank < population.ElementAt(opponent).Rank
                    ? population.ElementAt(player)
                    : population.ElementAt(opponent));
            }
            
            return temporaryPopulation;
        }
    }
}