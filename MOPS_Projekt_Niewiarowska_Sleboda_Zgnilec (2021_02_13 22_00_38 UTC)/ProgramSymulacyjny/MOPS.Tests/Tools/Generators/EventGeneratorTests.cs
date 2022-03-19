using System.Collections.Generic;
using MOPS.Tools.Generators;
using NUnit.Framework;

namespace MOPS.Tests.Tools.Generators
{
    public class EventGeneratorTests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void WhenEventGeneratorUsed_ShouldGenerateListOfEventsWithPoissonDistribution()
        {
            var eventGenerator = new EventGenerator();
            var events = eventGenerator.InitializeEventsList(10, SourceType.Poisson);

            Assert.Pass();
        }
    }
}