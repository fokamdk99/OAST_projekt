using System;
using System.Linq;
using OAST.Tools;
using Microsoft.Extensions.DependencyInjection;
using OAST.Events;
using OAST.Queue;
using OAST.Server;
using OAST.Simulator;

namespace OAST
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
                Console.WriteLine("Invalid number of input parameters!\n" +
                                  "Params: queueSize, numberOfRepetitions, " +
                                  "lambda, mi, numberOfPackages");
                
                return;
            }

            // e.g. 51, 
            Parameters.queueSize = Int32.Parse(args.ElementAt(0));
            int numberOfRepetitions = Int32.Parse(args.ElementAt(1));
            int lambda = Int32.Parse(args.ElementAt(2));
            int mi = Int32.Parse(args.ElementAt(3));
            Parameters.numberOfPackages = Int32.Parse(args.ElementAt(4));
            Parameters.numberOfSources = 1;
            
            simulator.Run(Parameters.queueSize, numberOfRepetitions, lambda, mi);
        }
    }
}
