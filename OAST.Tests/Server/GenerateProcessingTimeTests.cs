using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using OAST.Server;
using OAST.Tools.Generators;
using NUnit.Framework;

namespace OAST.Tests.Server
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
    }
}