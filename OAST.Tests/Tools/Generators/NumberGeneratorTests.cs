using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using OAST.Tools.Generators;
using NUnit.Framework;

namespace OAST.Tests.Tools.Generators
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
            List<double> results = new List<double>();
            
            for (int i = 0; i < 250; i++)
            {
                results.Add(_numberGenerator!.Generate(SourceType.Poisson, 300502 + i, 3));
            }

            double sum = 0;
            foreach (var number  in results)
            {
                sum += number;
            }

            double x = sum / 250; 
            Console.WriteLine(x);
            Assert.Less(x, 0.34);
            Assert.Greater(x, 0.31);
        }
    }
}