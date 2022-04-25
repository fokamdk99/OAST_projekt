using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using OAST.DemandAllocation.EvolutionAlgorithm;
using OAST.DemandAllocation.EvolutionTools;
using OAST.DemandAllocation.FileReader;
using OAST.DemandAllocation.Fitness;
using OAST.DemandAllocation.Topology;

namespace OAST.DemandAllocation.Tests.EvolutionAlgorithm
{
    public class ChromosomeDdapTests
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
            
            _fileReader.FileName = "./files/net4.txt";
            _fileReader.ReadFile();
            
            _topology = serviceProvider.GetRequiredService<ITopology>();
            _reproduction = serviceProvider.GetRequiredService<IReproduction>();
            _tools = serviceProvider.GetRequiredService<ITools>();
            _fitnessFunction = serviceProvider.GetRequiredService<IFitnessFunction>();
        }
        
        [Test]
        public void LinkLoads_ShouldBeProperlyCalculated1()
        {
            var chromosome = new Chromosome(_topology, _fitnessFunction, _tools.SetPathLoads());
            chromosome.PathLoads[0] = new List<int> { 3,0,0 };
            chromosome.PathLoads[1] = new List<int> { 4,0,0 };
            chromosome.PathLoads[2] = new List<int> { 3,2 };
            chromosome.PathLoads[3] = new List<int> { 2,0,0 };
            chromosome.PathLoads[4] = new List<int> { 3,0,0 };
            chromosome.PathLoads[5] = new List<int> { 4,0,0 };
            chromosome.CalculateLinkLoads();
            chromosome.CalculateMaxLoad();
            Assert.Pass();
        }
    }
}