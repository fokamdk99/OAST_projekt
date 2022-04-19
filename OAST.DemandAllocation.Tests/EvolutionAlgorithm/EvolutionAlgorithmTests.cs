using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using OAST.DemandAllocation.Demands;
using OAST.DemandAllocation.EvolutionAlgorithm;
using OAST.DemandAllocation.EvolutionTools;
using OAST.DemandAllocation.FileReader;
using OAST.DemandAllocation.Links;
using OAST.DemandAllocation.Topology;

namespace OAST.DemandAllocation.Tests.EvolutionAlgorithm
{
    public class EvolutionAlgorithmTests
    {
        private IFileReader _fileReader;
        private ITopology _topology;
        private IReproduction _reproduction;
        private IInheritance _inheritance;
        private ITools _tools;
        
        [SetUp]
        public void Setup()
        {
            var serviceProvider = new ServiceCollection()
                .AddDemandsFeature()
                .AddFileReaderFeature()
                .AddLinksFeature()
                .AddTopologyFeature()
                .AddEvolutionToolsFeature()
                .AddEvolutionAlgorithmFeature()
                .BuildServiceProvider();

            _fileReader = serviceProvider.GetRequiredService<IFileReader>();
            _topology = serviceProvider.GetRequiredService<ITopology>();
            _reproduction = serviceProvider.GetRequiredService<IReproduction>();
            _inheritance = serviceProvider.GetRequiredService<IInheritance>();
            _tools = serviceProvider.GetRequiredService<ITools>();
            
            _fileReader.FileName = "./files/net4.txt";
            _fileReader.ReadFile();
        }
        
        [Test]
        public void TestEvolutionAlgorithm()
        {
            var population = new List<Chromosome>();

            foreach (var value in Enumerable.Range(1, 10))
            {
                var chromosome = new Chromosome(_topology, _tools.SetPathLoads());
                chromosome.CalculateLinkLoads();
                population.Add(chromosome);
            }
            
            var reproductionSet = _reproduction.SelectReproductionSet(population);
            var chromosomesWithCrossovers = _tools.PerformCrossovers(reproductionSet);
            var chromosomesWithMutations = _tools.PerformMutations(chromosomesWithCrossovers);
            population = _inheritance.SelectInheritanceSet(chromosomesWithMutations, population);
            Assert.Pass();
        }
    }
}