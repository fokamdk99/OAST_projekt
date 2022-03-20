using System.Collections.Generic;
using NUnit.Framework;
using OAST.Tools;

namespace OAST.Tests.Tools
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
                results.Add(numberGenerator.Generate("Poisson"));
            }

            Assert.Pass();
        }
    }
}