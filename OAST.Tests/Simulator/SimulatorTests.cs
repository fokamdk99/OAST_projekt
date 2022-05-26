using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using OAST.Events;
using OAST.Queue;
using OAST.Server;
using OAST.Simulator;
using OAST.Tools;

namespace OAST.Tests.Simulator
{
    public class SimulatorTests
    {
        private ISimulator? _simulator;
        
        [SetUp]
        public void Setup()
        {
            IServiceProvider sericeProvider = new ServiceCollection()
                .AddEventFeature()
                .AddQueueFeature()
                .AddServerFeature()
                .AddToolsFeature()
                .AddSimulatorFeature()
                .BuildServiceProvider();

            _simulator = sericeProvider.GetRequiredService<ISimulator>();
        }
    }
}