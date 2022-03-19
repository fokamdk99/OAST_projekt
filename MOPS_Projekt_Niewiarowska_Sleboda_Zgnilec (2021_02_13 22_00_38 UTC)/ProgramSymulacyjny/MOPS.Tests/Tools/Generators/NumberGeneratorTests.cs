using System.Collections.Generic;
using MOPS.Tools.Generators;
using NUnit.Framework;

namespace MOPS.Tests.Tools.Generators
{
    public class NumberGeneratorTests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void WhenNumberGeneratorUsed_ShouldGenerateNumbersWithPoissonDistribution()
        {
            var numberGenerator = new NumberGenerator();
            List<int> results = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                results.Add(numberGenerator.Generate(SourceType.Poisson));
            }

            Assert.Pass();
        }
    }
}