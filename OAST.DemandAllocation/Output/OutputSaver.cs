using System;
using System.IO;
using System.Linq;
using OAST.DemandAllocation.EvolutionAlgorithm;
using OAST.DemandAllocation.Topology;

namespace OAST.DemandAllocation.Output
{
    public class OutputSaver : IOutputSaver
    {
        private readonly ITopology _topology;

        public OutputSaver(ITopology topology)
        {
            _topology = topology;
        }

        public void SaveResults(Chromosome chromosome, 
            string outputFileName, 
            Parameters parameters, 
            int numberOfIterations, 
            TimeSpan elapsedTime)
        {
            string output = "";
            string csvOutput = "";

            output += parameters.DescribeParameters();

            output += $"Number of iterations: {numberOfIterations}\n";
            output += $"Elapsed time: {elapsedTime.ToString()}\n";
            output += $"Fitness function value: {chromosome.MaxLoad}\n\n";

            var numberOfLinks = chromosome.LinkLoads.Count;
            output += $"{numberOfLinks}\n\n";

            for (int i = 0; i < _topology.Links.Count; i++)
            {
                var link = _topology.Links.ElementAt(i);
                var linkId = link.LinkId;
                var linkLoad = chromosome.LinkLoads.ElementAt(i);
                var numberOfFibers = (linkLoad / link.NumberOfLambdasInFibre) + 
                                     (linkLoad % link.NumberOfLambdasInFibre == 0 ? 0 : 1);
                output += $"{linkId} {linkLoad} {numberOfFibers}\n";
                
            }

            output += "\n";

            var numberOfDemands = _topology.Demands.Count;

            output += $"{numberOfDemands}\n\n";

            for (int i = 0; i < _topology.Demands.Count; i++)
            {
                var demand = _topology.Demands.ElementAt(i);
                var pathLoads = chromosome.PathLoads.ElementAt(i);
                
                output += $"{demand.DemandId} {pathLoads.Count}\n";
                csvOutput += $"{demand.DemandId},{pathLoads.Count},";

                int pathId = 1;
                
                foreach (var pathLoad in pathLoads)
                {
                    output += $"{pathId} {pathLoad}\n";
                    csvOutput += $"{pathLoad},";
                    pathId += 1;
                }

                output += "\n\n\n";
                csvOutput += "\n";
            }
            File.WriteAllText(outputFileName + ".txt", output);
            File.WriteAllText(outputFileName + ".csv", csvOutput);
        }
    }
}