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
    public class TimeCriteriaTests
    {
        private IFileReader _fileReader;
        private ITopology _topology;
        private IReproduction _reproduction;
        private ITools _tools;
        private IEvolutionAlgorithm<TimeCriteria> _evolutionAlgorithm;

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
                .AddRandomNumberGeneratorFeature()
                .BuildServiceProvider();

            _fileReader = serviceProvider.GetRequiredService<IFileReader>();
            _topology = serviceProvider.GetRequiredService<ITopology>();
            _reproduction = serviceProvider.GetRequiredService<IReproduction>();
            _tools = serviceProvider.GetRequiredService<ITools>();

            _fileReader.FileName = "./files/net4.txt";
            _fileReader.ReadFile();
            
            _evolutionAlgorithm = serviceProvider.GetRequiredService<IEvolutionAlgorithm<TimeCriteria>>();
        }

        [Test]
        public void TestTimeCriteria()
        {
            var generationsCriteria = new TimeCriteria(6);
            _evolutionAlgorithm.SetParams(generationsCriteria);
            
            _evolutionAlgorithm.Run(CriteriaTestTools.GetTestParameters(), EvaluateTimeCriteria.Evaluate, null, null);
            Assert.GreaterOrEqual(generationsCriteria.ElapsedTime, 6);
        }
    }
}