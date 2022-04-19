using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using OAST.DemandAllocation.Demands;
using OAST.DemandAllocation.EvolutionAlgorithm;
using OAST.DemandAllocation.EvolutionTools;
using OAST.DemandAllocation.FileReader;
using OAST.DemandAllocation.Links;
using OAST.DemandAllocation.Topology;

namespace OAST.DemandAllocation.Tests.EvolutionTools
{
    public class EvolutionToolsTests
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
        public void Crossover_ShouldCreateNewChromosomeConsistingOfPartsOfTwoParents()
        {
            var parent1 = new Chromosome(_topology, _tools.SetPathLoads());
            var parent2 = new Chromosome(_topology, _tools.SetPathLoads());
            var crossover = _tools.PerformCrossover(parent1, parent2);
            Assert.Pass();
        }
        
        [Test]
        public void Mutation_ShouldAlterGeneOfAnExistingChromosome()
        {
            var chromosome = new Chromosome(_topology, _tools.SetPathLoads());
            var crossover = _tools.PerformMutation(chromosome);
            Assert.Pass();
        }
    }
}