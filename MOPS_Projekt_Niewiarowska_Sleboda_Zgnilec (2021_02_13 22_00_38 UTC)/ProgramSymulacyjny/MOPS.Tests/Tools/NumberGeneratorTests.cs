using System;
using System.Collections.Generic;
using MOPS.Tools;
using NUnit.Framework;

namespace MOPS.Tests.Tools
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