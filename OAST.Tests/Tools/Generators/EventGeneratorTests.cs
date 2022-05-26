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
    }
}