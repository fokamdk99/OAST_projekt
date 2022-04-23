using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using OAST.DemandAllocation.EvolutionAlgorithm;
using OAST.DemandAllocation.EvolutionTools;
using OAST.DemandAllocation.FileReader;
using OAST.DemandAllocation.Fitness;
using OAST.DemandAllocation.Output;
using OAST.DemandAllocation.Topology;

namespace OAST.DemandAllocation.Tests.Output
{
    public class OutputTests
    {
        private IOutputSaver _outputSaver;
        private IFileReader _fileReader;
        private ITopology _topology;
        private ITools _tools;
        private IFitnessFunction _fitnessFunction;
        
        [SetUp]
        public void Setup()
        {
            var serviceProvider = new ServiceCollection()
                .AddDemandAllocationFeature(true)
                .BuildServiceProvider();

            _fileReader = serviceProvider.GetRequiredService<IFileReader>();
            _fileReader.FileName = "./files/net4.txt";
            _fileReader.ReadFile();
            
            _outputSaver = serviceProvider.GetRequiredService<IOutputSaver>();
            _topology = serviceProvider.GetRequiredService<ITopology>();
            _tools = serviceProvider.GetRequiredService<ITools>();
            _fitnessFunction = serviceProvider.GetRequiredService<IFitnessFunction>();
        }

        [Test]
        public void WhenBestSolutionFound_ShouldSaveDataToFile()
        {
            var chromosome = new Chromosome(_topology, _fitnessFunction, _tools.SetPathLoads());
            chromosome.PathLoads[0] = new List<int> { 0,3,0 };
            chromosome.PathLoads[1] = new List<int> { 2,2,0 };
            chromosome.PathLoads[2] = new List<int> { 3,2 };
            chromosome.PathLoads[3] = new List<int> { 1,1,0 };
            chromosome.PathLoads[4] = new List<int> { 1,2,0 };
            chromosome.PathLoads[5] = new List<int> { 1,2,1 };
            chromosome.CalculateLinkLoads();

            string fileName = $"../../../files/outputs/OAST2_output_{DateTime.UtcNow.ToString("yyyyMMddTHHmmss")}.txt";
            _outputSaver.SaveResults(chromosome, fileName);
        }
    }
}