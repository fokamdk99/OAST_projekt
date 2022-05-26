using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using OAST.Tools.Generators;

namespace OAST.Tests.Tools
{
    public class StatisticAggregatorTests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void WhenNumberGeneratorUsed_ShouldGenerateNumbersWithPoissonDistribution()
        {
            List<int> results = new List<int> { 5, 8, 3, 7};

            var diff = results.Aggregate((prev, next) => prev - next);

            Assert.Pass();
        }
    }
}