using System.Collections.Generic;
using System.Linq;
using OAST.DemandAllocation.EvolutionAlgorithm;
using OAST.DemandAllocation.Topology;

namespace OAST.DemandAllocation.Fitness
{
    public class DdapFitnessFunction : IFitnessFunction
    {
        private readonly ITopology _topology;

        public DdapFitnessFunction(ITopology topology)
        {
            _topology = topology;
        }

        public int CalculateMaxLoad(Chromosome chromosome)
        {
            List<int> linkLoads = new List<int>();
            
            foreach (var linkLoad in chromosome.LinkLoads
                .Select((value, linkIndex) => new {value, linkIndex}))
            {
                var link = _topology.Links.ElementAt(linkLoad.linkIndex);
                var numberOfFibers = (linkLoad.value / link.NumberOfLambdasInFibre) +
                                     (linkLoad.value % link.NumberOfLambdasInFibre == 0 ? 0 : 1);
                linkLoads.Add(numberOfFibers * (int)link.FibrePairCost); // czy na pewno? moze dac linkLoad jako float?
            }

            var ddapResult = linkLoads.Sum();
            chromosome.MaxLoad = ddapResult;

            return ddapResult;
        }
    }
}