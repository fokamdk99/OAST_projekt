using System;
using Microsoft.Extensions.DependencyInjection;
using OAST.Tools.Generators;
using NUnit.Framework;

namespace OAST.Tests.Tools.Generators
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
            var events = eventGenerator.InitializeEventsList(10000, SourceType.Poisson, 300, 2);
            var time = 0.0;
            for (int i= 1; i < events.Count; i++)
            {
                time += (events[i].Time - events[i-1].Time);
            }

            time = time / events.Count;
            Console.WriteLine(time);
            Assert.Greater(time, 0.48);
            Assert.Less(time, 0.52);
            Assert.Pass();
        }
    }
}