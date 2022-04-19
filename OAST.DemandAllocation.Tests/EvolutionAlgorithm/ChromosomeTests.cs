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
    public class ChromosomeTests
    {
        private IFileReader _fileReader;
        private ITopology _topology;
        private IReproduction _reproduction;
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
            _tools = serviceProvider.GetRequiredService<ITools>();
            
            _fileReader.FileName = "./files/net4.txt";
            _fileReader.ReadFile();
        }

        [Test]
        public void LinkLoads_ShouldBeProperlyCalculated()
        {
            var chromosome = new Chromosome(_topology, _tools.SetPathLoads());
            chromosome.PathLoads[0] = new List<int> { 0,3,0 };
            chromosome.PathLoads[1] = new List<int> { 2,2,0 };
            chromosome.PathLoads[2] = new List<int> { 3,2 };
            chromosome.PathLoads[3] = new List<int> { 1,1,0 };
            chromosome.PathLoads[4] = new List<int> { 1,2,0 };
            chromosome.PathLoads[5] = new List<int> { 1,2,1 };
            chromosome.CalculateLinkLoads();
            Assert.That(chromosome.LinkLoads.ElementAt(0) == 7);
            Assert.That(chromosome.LinkLoads.ElementAt(1) == 9);
            Assert.That(chromosome.LinkLoads.ElementAt(2) == 10); // 3 + 2 + 0 + (1 + 0) + 2 + 2
            Assert.That(chromosome.LinkLoads.ElementAt(3) == 7);
            Assert.That(chromosome.LinkLoads.ElementAt(4) == 5);
        }

        [Test]
        public void Ranks_ShouldBeProperlyCalculated()
        {
            var population = new List<Chromosome>();

            foreach (var value in Enumerable.Range(1, 5))
            {
                var chromosome = new Chromosome(_topology, _tools.SetPathLoads());
                chromosome.CalculateLinkLoads();
                population.Add(chromosome);
            }
            
            _reproduction.CalculateRanks(population);
            Assert.Pass();
        }

        [Test]
        public void ReproductionSet_ShouldContainCurrentPopulationMembers()
        {
            var population = new List<Chromosome>();

            foreach (var value in Enumerable.Range(1, 5))
            {
                var chromosome = new Chromosome(_topology, _tools.SetPathLoads());
                chromosome.CalculateLinkLoads();
                population.Add(chromosome);
                
            }

            var reproductionSet = _reproduction.SelectReproductionSet(population);
            Assert.Pass();
        }
    }
}