using System;
using Microsoft.Extensions.DependencyInjection;
using MOPS.Tools.Generators;
using NUnit.Framework;

namespace MOPS.Tests.Tools.Generators
{
    public class EventGeneratorTests
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
        public void WhenEventGeneratorUsed_ShouldGenerateListOfEventsWithPoissonDistribution()
        {
            var eventGenerator = new EventGenerator(_numberGenerator!);
            var events = eventGenerator.InitializeEventsList(10, SourceType.Poisson, 300, 3);

            Assert.Pass();
        }
    }
}