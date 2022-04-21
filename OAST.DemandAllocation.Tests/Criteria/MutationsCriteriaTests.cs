using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using OAST.DemandAllocation.Criteria;
using OAST.DemandAllocation.Demands;
using OAST.DemandAllocation.EvolutionAlgorithm;
using OAST.DemandAllocation.EvolutionTools;
using OAST.DemandAllocation.FileReader;
using OAST.DemandAllocation.Links;
using OAST.DemandAllocation.RandomNumberGenerator;
using OAST.DemandAllocation.Topology;

namespace OAST.DemandAllocation.Tests.Criteria
{
    public class MutationsCriteriaTests
    {
        private IServiceProvider? _serviceProvider;
        private ITools? _tools;
        private ITopology? _topology;
        private IEvolutionAlgorithm<MutationsCriteria> _evolutionAlgorithm;

        private IServiceCollection? _serviceCollection;
        
        [SetUp]
        public void Setup()
        {
            _serviceCollection = new ServiceCollection()
                .AddDemandsFeature()
                .AddFileReaderFeature()
                .AddLinksFeature()
                .AddTopologyFeature()
                .AddEvolutionToolsFeature()
                .AddRandomNumberGeneratorFeature()
                .AddEvolutionAlgorithmFeature();

            _serviceProvider = _serviceCollection.BuildServiceProvider();

            var fileReader = _serviceProvider.GetRequiredService<IFileReader>();
            _tools = _serviceProvider.GetRequiredService<ITools>();
            _topology = _serviceProvider.GetRequiredService<ITopology>();

            fileReader.FileName = "./files/net4.txt";
            fileReader.ReadFile();
            
            _evolutionAlgorithm = _serviceProvider.GetRequiredService<IEvolutionAlgorithm<MutationsCriteria>>();
        }

        [Test]
        public void TestMutationsCriteria()
        {
            var randomNumberGenerator = new Mock<RandomGenerator>().As<IRandomNumberGenerator>();
            randomNumberGenerator.Setup(x => x.GenerateRandomFloatNumber()).Returns(0.9f);

            var serviceProvider = _serviceCollection!.AddSingleton(randomNumberGenerator.Object).BuildServiceProvider();
            var tools = serviceProvider.GetRequiredService<ITools>();
            
            var population = new List<Chromosome>();

            foreach (var value in Enumerable.Range(1, 5))
            {
                var chromosome = new Chromosome(_topology!, _tools!.SetPathLoads());
                chromosome.CalculateLinkLoads();
                population.Add(chromosome);
            }

            var mutationsCriteria = new MutationsCriteria(5);
            
            var result = tools.PerformMutations(population, mutationsCriteria);
            randomNumberGenerator.Verify(x => x.GenerateRandomFloatNumber(), Times.Exactly(5));
            Assert.AreEqual(mutationsCriteria.CurrentMutation, 5);

            Assert.Pass();
        }
        
        [Test]
        public void TestGenerationsCriteria()
        {
            var mutationsCriteria = new MutationsCriteria(8);
            _evolutionAlgorithm.SetParams(mutationsCriteria);
            
            _evolutionAlgorithm.Run(CriteriaTestTools.GetTestParameters(), EvaluateMutationsCriteria.Evaluate, null, null);
            Assert.AreEqual(mutationsCriteria.CurrentMutation, 8);
        }
    }
}