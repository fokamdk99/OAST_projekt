using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using OAST.DemandAllocation.BruteForceAlgorithm;
using OAST.DemandAllocation.Demands;
using OAST.DemandAllocation.EvolutionAlgorithm;
using OAST.DemandAllocation.EvolutionTools;
using OAST.DemandAllocation.FileReader;
using OAST.DemandAllocation.Links;
using OAST.DemandAllocation.Topology;

namespace OAST.DemandAllocation
{
    class Program
    {

        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddDemandsFeature()
                .AddFileReaderFeature()
                .AddLinksFeature()
                .AddTopologyFeature()
                .AddEvolutionToolsFeature()
                .AddEvolutionAlgorithmFeature()
                .BuildServiceProvider();

            if (args.Length != 6 || args.Length != 1)
            {
                Console.WriteLine("Valid formats:\n" +
                                  "<algorithm> [<number items in initial population> <crossover probability> " +
                                  "<mutation probability> <seed> <stop criteria>]\n" +
                                  "brute force algorithm - 1\n" +
                                  "evolution algorithm - 2\n" +
                                  "stop criteria: time - 1, number of generations - 2, number of mutations - 3, best chromosome - 4");
                return;
            }

            if (args.Length == 6)
            {
                var parameters = new Parameters
                {
                    Mi = Int32.Parse(args.ElementAt(1)),
                    CrossoverProbability = Int32.Parse(args.ElementAt(2)),
                    MutationProbability = Int32.Parse(args.ElementAt(3)),
                    Seed = Int32.Parse(args.ElementAt(4)),
                    StopCriteria = Int32.Parse(args.ElementAt(5))
                };
                
                var evolutionAlgorithm = serviceProvider.GetRequiredService<IEvolutionAlgorithm>();
                evolutionAlgorithm.Run();
            }

            if (args.Length == 1)
            {
                var bruteForceAlgorithm = serviceProvider.GetRequiredService<IBruteForceAlgorithm>();
                bruteForceAlgorithm.Run();
            }

            //TODO: save results to file; evaluate stop criteria
            // mi razy inicjalizuj tablicę: os x -> demands, os y -> paths
            // x = funkcje alokacji olej, Ostrowski nie wiedzial nawet czym sie rozni od inicjalizacji
            // oblicz f(x), czyli suma po e (koszt jednego modułu * y(e,x)), tudziez koszt wszystkich modulow
            // ustaw zbior poczatkowy jako x
            // dopoki nie kryterium stopu:
            // powtarzaj lambda razy reprodukcje, czyli wybor pozadanych osobnikow (np. selekcja turniejowa), ktorzy beda poddani krzyzowaniu i mutacji
            // ustaw x jako losowe wartosci z Pt
            // ustaw x' jako kopie x
            // dodaj x' do zbioru Ot
            // podziel Ot na lambda/2 rozlocznych dwuelementowych podzbiorow
            // dla kazdego podzbioru, jesli spelniony jest warunek (prawdopodobienstwo krzyzowania, zazwyczaj wysokie, powyzej 0.7), wykonaj krzyzowanie, czyli randomowe polaczenie ze soba wartosci obydwu chromosomow
            // dla kazdej wartosci z Ot, jesli spelniony jest warunek (prawdopodobienstwo mutacji, zazwyczaj niskie, ponizej 0.3), wykonaj mutacje, czyli zabierz jedna jednostke z danej sciezki w demandzie i przypisz ja innej sciezce w tym samym demandzie
            // oblicz f(x)
        }
    }
}