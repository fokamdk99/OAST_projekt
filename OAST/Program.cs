using System;
using System.Linq;
using OAST.Tools;
using Microsoft.Extensions.DependencyInjection;
using OAST.Events;
using OAST.Queue;
using OAST.Server;
using OAST.Simulator;
using OAST.Tools.Generators;

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
                .AddSingleton<IParameters, Parameters>()
                .BuildServiceProvider();

            var simulator = serviceProvider.GetRequiredService<ISimulator>();
            var parameters = serviceProvider.GetRequiredService<IParameters>();

            if (args.Length != 6)
            {
                Console.WriteLine("Invalid number of input parameters!\n" +
                                  "Params: queueSize, numberOfRepetitions, " +
                                  "lambda, mi, simulationTime, blockSize");
                
                return;
            }

            parameters.QueueSize = Int32.Parse(args.ElementAt(0));
            parameters.NumberOfSimulations = Int32.Parse(args.ElementAt(1));
            parameters.Lambda = Double.Parse(args.ElementAt(2));
            parameters.Mi = Double.Parse(args.ElementAt(3));
            parameters.SimulationTime = Int32.Parse(args.ElementAt(4));
            parameters.BlockSize = Int32.Parse(args.ElementAt(5));
            parameters.SetSourceType(SourceType.Uniform);
            simulator.Run();
        }
    }
}
