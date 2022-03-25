using System;
using System.Linq;
using MOPS.Tools;
using Microsoft.Extensions.DependencyInjection;
using MOPS.Events;
using MOPS.Queue;
using MOPS.Server;
using MOPS.Simulator;

namespace MOPS
{
    class Program
    {

        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddEventFeature()
                .AddQueueFeature()
                .AddServerFeature()
                .AddToolsFeature()
                .AddSimulatorFeature()
                .BuildServiceProvider();

            var simulator = serviceProvider.GetRequiredService<ISimulator>();

            if (args.Length != 5)
            {
                Console.WriteLine("Invalid number of input parameters!" +
                                  "Params: queueSize, serverBitRate, numberOfRepetitions, " +
                                  "lambda, numberOfPackages");
            }

            // e.g. 51, 
            Parameters.queueSize = Int32.Parse(args.ElementAt(0));
            Parameters.serverBitRate = Int32.Parse(args.ElementAt(1));
            int numberOfRepetitions = Int32.Parse(args.ElementAt(2));
            int lambda = Int32.Parse(args.ElementAt(3));
            Parameters.numberOfPackages = Int32.Parse(args.ElementAt(4));
            Parameters.numberOfSources = 1;
            
            simulator.Run(Parameters.queueSize, Parameters.serverBitRate, numberOfRepetitions, lambda);
        }
    }
}
