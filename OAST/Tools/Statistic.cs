using System;
using System.Collections.Generic;

namespace OAST.Tools
{
    public static class Statistic
    {
        public static int NumberOfReceivedPackages = 0; // wtf czemu ma sluzyc ta zmienna, dlaczego od razu zwiekszamy o jeden gdy przychodzi pakiet
        // zanim w ogole sprawdzimy czy jest miejsce w kolejce

        public static int NumberOfLostPackages = 0;

        public static int NumberOfPackagesInQueue = 0; // wtf to jakis licznik zwiekszajacy sie przez caly czas trwania symulacji?

        public static double Time = 0;

        public static double AverageTimeinQueue = 0;

        public static double AverageNumberOfPackagesInQueue = 0;

        public static double SimulationTime = 0;

        public static double ServerLoad = 0;

        public static double ServerLoadTime = 0;

        public static List<GlobalStatistic> GlobalList = new List<GlobalStatistic>();
        
        public static double PercentOfSuccess = 0;

        // -------------------------------------Inceremnt--------------------------------
        public static void IncrementNumberOfReceivedPackages()
        {
            NumberOfReceivedPackages = NumberOfReceivedPackages + 1;
        }

        public static void IncrementNumberOfLostPackages()
        {
            NumberOfLostPackages = NumberOfLostPackages + 1;
        }

        public static void IncrementNumberOfPackagesInQueue()
        {
            NumberOfPackagesInQueue = NumberOfPackagesInQueue + 1;
        }

        public static void AddAverageTimeinQueue(double element)
        {
            AverageTimeinQueue += element;
        }

        public static void ResetStatistics()
        {
            NumberOfReceivedPackages = 0;

            NumberOfLostPackages = 0;

            NumberOfPackagesInQueue = 0;

            Time = 0;

            AverageTimeinQueue = 0;

            AverageNumberOfPackagesInQueue = 0;

            SimulationTime = 0;

            ServerLoad = 0;

            ServerLoadTime = 0;
        }

//--------------------------------------------------------Calculate-------------------------
        public static double CalculateAverageTime()
        {
            double sum = AverageTimeinQueue;
            double result;

            result = sum / NumberOfPackagesInQueue;
            AverageTimeinQueue = result;
            return result;
        }

        public static double CalculateServerLoad()
        {
            ServerLoad = ServerLoadTime / Statistic.SimulationTime;
            return ServerLoad;
        }

        public static void CalculateServerLoadTime(double busyStart, double busyStop)
        {
            ServerLoadTime = ServerLoadTime + (busyStop - busyStart);
        }

        //-------------------------------------Print----------------------------------------

        public static void PrintStatistics()
        {
            Console.WriteLine(
                $"[STATISTIC]\nNumberOfRecivedPackage: {NumberOfReceivedPackages}\nNumberOfLostPackage: {NumberOfLostPackages}\n\n");
        }

        public static void PrintAverageTimeInQueue()
        {
            Console.WriteLine($"\nAverage Time in Queue: {CalculateAverageTime()}\n\n");
        }

        public static void PrintServerLoad()
        {
            Console.WriteLine($"[Server Load] {CalculateServerLoad()}\n\n");
        }

        //-----------------------------GlobalList----------------------------

        public static void Calculate()
        {
            foreach (var e in GlobalList)
            {
                NumberOfReceivedPackages = e.NumberOfReceivedPackages + NumberOfReceivedPackages;
                NumberOfLostPackages = e.NumberOfLostPackages + NumberOfLostPackages;
                NumberOfPackagesInQueue = e.NumberOfPackagesInQueue + NumberOfPackagesInQueue;
                AverageTimeinQueue = e.AverageTimeInQueue + AverageTimeinQueue;
                AverageNumberOfPackagesInQueue = e.AverageNumberOfPackagesInQueue + AverageNumberOfPackagesInQueue;
                SimulationTime = e.SimulationTime + SimulationTime;
                ServerLoad = e.ServerLoad + ServerLoad;
            }

            NumberOfPackagesInQueue = NumberOfPackagesInQueue / 100;
            AverageTimeinQueue = AverageTimeinQueue / 100;
            AverageNumberOfPackagesInQueue = AverageNumberOfPackagesInQueue / 100;
            ServerLoad = ServerLoad / 100;
            PercentOfSuccess = (((float) Statistic.NumberOfReceivedPackages - (float) Statistic.NumberOfLostPackages) /
                (float) (Statistic.NumberOfReceivedPackages) * 100);
        }
    }
}