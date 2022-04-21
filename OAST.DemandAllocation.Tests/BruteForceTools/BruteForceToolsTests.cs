using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using OAST.DemandAllocation.BruteForceTools;
using OAST.DemandAllocation.Demands;
using OAST.DemandAllocation.EvolutionAlgorithm;
using OAST.DemandAllocation.EvolutionTools;
using OAST.DemandAllocation.FileReader;
using OAST.DemandAllocation.Links;
using OAST.DemandAllocation.Topology;

namespace OAST.DemandAllocation.Tests.BruteForceTools
{
    public class BruteForceToolsTests
    {
        private IFileReader _fileReader;
        private ITopology _topology;
        private IReproduction _reproduction;
        private IBfTools _tools;
        
        [SetUp]
        public void Setup()
        {
            var serviceProvider = new ServiceCollection()
                .AddDemandsFeature()
                .AddFileReaderFeature()
                .AddLinksFeature()
                .AddTopologyFeature()
                .AddBfToolsFeature()
                .BuildServiceProvider();

            _fileReader = serviceProvider.GetRequiredService<IFileReader>();
            _topology = serviceProvider.GetRequiredService<ITopology>();
            _tools = serviceProvider.GetRequiredService<IBfTools>();
            
            _fileReader.FileName = "./files/net4.txt";
            _fileReader.ReadFile();
        }
        
        [Test]
        public void TestRecursion()
        {
            var bruteTools = new BfTools(_topology);
            List<int> vector = new List<int> {0, 0, 0, 0}; 
            bruteTools.Recursion(vector, 0, 5, 0);
            Assert.Pass();
        }

        [Test]
        public void TestCartesian()
        {
            var firstList = new List<int> {1,2};
            var secondList = new List<int>{3,4};
            var thirdList = new List<int>{5,6,7};
            List<List<int>> listOfLists = new List<List<int>>();
            listOfLists.Add(firstList);
            listOfLists.Add(secondList);
            listOfLists.Add(thirdList);
            var query = firstList.SelectMany(x => secondList, (x, y) => new { x, y });

            var result = _tools.GetPermutations(listOfLists);
            Assert.Pass();
        }

        [Test]
        public void TestChromosomeGeneration()
        {
            var population = _tools.GenerateAllPossibleChromosomes();
            Assert.Pass();
        }
    }
}