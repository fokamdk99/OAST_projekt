using System.Collections.Generic;
using OAST.DemandAllocation.EvolutionAlgorithm;

namespace OAST.DemandAllocation.EvolutionTools
{
    public class Inheritance : IInheritance
    {
        private readonly IReproduction _reproduction;

        public Inheritance(IReproduction reproduction)
        {
            Eta = 2;
            _reproduction = reproduction;
        }
        
        public int Eta { get; set; }
        public List<Chromosome> SelectInheritanceSet(List<Chromosome> temporaryPopulation, List<Chromosome> currentPopulation)
        {
            List<Chromosome> inheritanceSet = new List<Chromosome>();

            _reproduction.CalculateRanks(temporaryPopulation);
            var bestOffspring = currentPopulation.GetRange(0, Eta);
            inheritanceSet.AddRange(bestOffspring);
            inheritanceSet.AddRange(temporaryPopulation.GetRange(0, currentPopulation.Count-Eta));
            
            _reproduction.CalculateRanks(inheritanceSet);

            return inheritanceSet;
        }
    }
}