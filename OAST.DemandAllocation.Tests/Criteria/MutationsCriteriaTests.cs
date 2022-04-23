using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using OAST.DemandAllocation.Criteria;
using OAST.DemandAllocation.EvolutionAlgorithm;
using OAST.DemandAllocation.EvolutionTools;
using OAST.DemandAllocation.FileReader;
using OAST.DemandAllocation.Fitness;
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
        private IFitnessFunction? _fitnessFunction;

        private IServiceCollection? _serviceCollection;
        
        [SetUp]
        public void Setup()
        {
            _serviceCollection = new ServiceCollection()
                .AddDemandAllocationFeature(true);

            _serviceProvider = _serviceCollection.BuildServiceProvider();

            var fileReader = _serviceProvider.GetRequiredService<IFileReader>();
            
            fileReader.FileName = "./files/net4.txt";
            fileReader.ReadFile();
            
            _tools = _serviceProvider.GetRequiredService<ITools>();
            _topology = _serviceProvider.GetRequiredService<ITopology>();
            _evolutionAlgorithm = _serviceProvider.GetRequiredService<IEvolutionAlgorithm<MutationsCriteria>>();
            _fitnessFunction = _serviceProvider.GetRequiredService<IFitnessFunction>();
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
                var chromosome = new Chromosome(_topology!, _fitnessFunction!, _tools!.SetPathLoads());
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