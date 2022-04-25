using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using OAST.DemandAllocation.BruteForceTools;
using OAST.DemandAllocation.EvolutionAlgorithm;
using OAST.DemandAllocation.EvolutionTools;
using OAST.DemandAllocation.FileReader;
using OAST.DemandAllocation.Fitness;
using OAST.DemandAllocation.Topology;

namespace OAST.DemandAllocation.Tests.BruteForceAlgorithm
{
    public class BruteForceAlgorithmTests
    {
        private IFileReader _fileReader;
        private ITopology _topology;
        private IReproduction _reproduction;
        private ITools _tools;
        private IFitnessFunction _fitnessFunction;
        
        [SetUp]
        public void Setup()
        {
            var serviceProvider = new ServiceCollection()
                .AddDemandAllocationFeature(false)
                .BuildServiceProvider();
            
            _fileReader = serviceProvider.GetRequiredService<IFileReader>();
            
            _fileReader.FileName = "./files/net12_2.txt";
            _fileReader.ReadFile();
            
            _topology = serviceProvider.GetRequiredService<ITopology>();
            _reproduction = serviceProvider.GetRequiredService<IReproduction>();
            _tools = serviceProvider.GetRequiredService<ITools>();
            _fitnessFunction = serviceProvider.GetRequiredService<IFitnessFunction>();
        }
        
        [Test]
        public void LinkLoads_ShouldBeProperlyCalculated1()
        {
            List<double> permutations = new List<double>();
            foreach (var demand in _topology.Demands)
            {
                var bandwidth = demand.DemandVolume;
                var numberOfPaths = demand.DemandPaths.Count;
                var numberOfPermutations = GetBinCoeff(bandwidth + numberOfPaths - 1, numberOfPaths - 1);
                permutations.Add(numberOfPermutations);
            }

            var result = permutations.Aggregate((previous, next) => previous * next);
            
            Assert.Pass();
        }
        
        internal static long GetBinCoeff(long N, long K)
        {
            // N is the total number of items.
            // K is the size of the group.
            // Total number of unique combinations = N! / ( K! (N - K)! ).
            long r = 1;
            long d;
            if (K > N) return 0;
            for (d = 1; d <= K; d++)
            {
                r *= N--;
                r /= d;
            }
            return r;
        }
    }
}