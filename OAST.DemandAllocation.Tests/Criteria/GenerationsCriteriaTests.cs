using Microsoft.Extensions.DependencyInjection;
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
    public class GenerationsCriteriaTests
    {
        private IFileReader _fileReader;
        private ITopology _topology;
        private IReproduction _reproduction;
        private ITools _tools;
        private IEvolutionAlgorithm<GenerationsCriteria> _evolutionAlgorithm;

        [SetUp]
        public void Setup()
        {
            var serviceProvider = new ServiceCollection()
                .AddDemandAllocationFeature(true)
                .BuildServiceProvider();

            _fileReader = serviceProvider.GetRequiredService<IFileReader>();
            _topology = serviceProvider.GetRequiredService<ITopology>();
            _reproduction = serviceProvider.GetRequiredService<IReproduction>();
            _tools = serviceProvider.GetRequiredService<ITools>();

            _fileReader.FileName = "./files/net4.txt";
            _fileReader.ReadFile();
            
            _evolutionAlgorithm = serviceProvider.GetRequiredService<IEvolutionAlgorithm<GenerationsCriteria>>();
        }

        [Test]
        public void TestGenerationsCriteria()
        {
            var generationsCriteria = new GenerationsCriteria(8);
            _evolutionAlgorithm.SetParams(generationsCriteria);
            
            _evolutionAlgorithm.Run(CriteriaTestTools.GetTestParameters(), EvaluateGenerationsCriteria.Evaluate, null, null);
            Assert.AreEqual(generationsCriteria.CurrentGeneration, 8);
        }
    }
}