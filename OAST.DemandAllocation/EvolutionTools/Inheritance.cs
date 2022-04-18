using System.Collections.Generic;
using OAST.DemandAllocation.EvolutionAlgorithm;
using OAST.DemandAllocation.Topology;

namespace OAST.DemandAllocation.EvolutionTools
{
    public class Inheritance : IInheritance
    {
        private readonly IReproduction _reproduction;
        private readonly ITopology _topology;
        
        public Inheritance(IReproduction reproduction, 
            ITopology topology)
        {
            Eta = 2;
            _reproduction = reproduction;
            _topology = topology;
        }
        
        public int Eta { get; set; }
        public List<Chromosome> SelectInheritanceSet(List<Chromosome> temporaryPopulation, List<Chromosome> currentPopulation)
        {
            var bestOffspring = currentPopulation.GetRange(0, Eta);
            temporaryPopulation.AddRange(bestOffspring);
            
            _reproduction.CalculateRanks(temporaryPopulation);

            return temporaryPopulation.GetRange(0, currentPopulation.Count);
        }
    }
}