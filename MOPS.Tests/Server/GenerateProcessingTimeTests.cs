using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using MOPS.Server;
using MOPS.Tools.Generators;
using NUnit.Framework;

namespace MOPS.Tests.Server
{
    public class GenerateProcessingTimeTests
    {
        private ICustomServer? _customServer;
        
        [SetUp]
        public void Setup()
        {
            IServiceProvider sericeProvider = new ServiceCollection()
                .AddSingleton<INumberGenerator, NumberGenerator>()
                .AddSingleton<ICustomServer, CustomServer>()
                .BuildServiceProvider();

            _customServer = sericeProvider.GetRequiredService<ICustomServer>();
        }

        [Test]
        public void WhenProcessingTimeGeneratorUsed_ShouldGenerateNumberWithPoissonDistribution()
        {
            _customServer!.SetMi(10);
            List<double> results = new List<double>();
            for (int i = 0; i < 20; i++)
            {
                results.Add(_customServer!.GenerateProcessingTime(SourceType.Poisson, 300501 + i));
            }

            Assert.Pass();
        }
    }
}