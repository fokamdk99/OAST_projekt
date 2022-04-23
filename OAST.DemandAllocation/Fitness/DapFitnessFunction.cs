using System.Collections.Generic;
using System.Linq;
using OAST.DemandAllocation.EvolutionAlgorithm;
using OAST.DemandAllocation.Topology;

namespace OAST.DemandAllocation.Fitness
{
    public class DapFitnessFunction : IFitnessFunction
    {
        private readonly ITopology _topology;

        public DapFitnessFunction(ITopology topology)
        {
            _topology = topology;
        }

        public int CalculateMaxLoad(Chromosome chromosome)
        {
            List<int> linkLoads = new List<int>();
            
            foreach (var linkLoad in chromosome.LinkLoads
                .Select((value, linkIndex) => new {value, linkIndex}))
            {
                linkLoads.Add(linkLoad.value - _topology.Links.ElementAt(linkLoad.linkIndex).LinkCapacity); // maximum (over all links)
            }

            var dapResult = linkLoads.Max();
            chromosome.MaxLoad = dapResult;

            return dapResult;
        }
    }
}