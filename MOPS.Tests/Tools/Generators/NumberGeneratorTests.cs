using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using MOPS.Tools.Generators;
using NUnit.Framework;

namespace MOPS.Tests.Tools.Generators
{
    public class NumberGeneratorTests
    {
        private INumberGenerator? _numberGenerator;
        
        [SetUp]
        public void Setup()
        {
            IServiceProvider sericeProvider = new ServiceCollection()
                .AddSingleton<INumberGenerator, NumberGenerator>()
                .BuildServiceProvider();

            _numberGenerator = sericeProvider.GetRequiredService<INumberGenerator>();
        }

        [Test]
        public void WhenNumberGeneratorUsed_ShouldGenerateNumbersWithPoissonDistribution()
        {
            List<int> results = new List<int>();
            for (int i = 0; i < 20; i++)
            {
                results.Add(_numberGenerator!.Generate(SourceType.Poisson, 300501 + i, 8));
            }

            Assert.Pass();
        }
    }
}