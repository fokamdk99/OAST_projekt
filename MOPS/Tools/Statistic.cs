using System;
using System.Collections.Generic;

namespace MOPS.Tools
{
    public static class Statistic
    {
        public static int NumberOfReceivedPackages = 0;

        public static int NumberOfLostPackages = 0;

        public static int NumberOfPackagesinQueue = 0;

        public static double Time = 0;

        public static int PackagesInSimulation = 0;

        private static List<double> AverageTimeinQueueList = new List<double>();

        public static double AverageTimeinQueue = 0;

        private static Dictionary<int, double> AveragePackageInQueueList = new Dictionary<int, double>();

        public static double AveragePackageInQueue = 0;

        public static double SimulationTime = 0;

        public static double ServerLoad = 0;

        public static double ServerLoadTime = 0;

        public static List<GlobalStatistic> GlobalList = new List<GlobalStatistic>();

        public static double ProgramTime = 0;
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
            NumberOfPackagesinQueue = NumberOfPackagesinQueue + 1;
        }

        public static void AddAverageTimeinQueue(double element)
        {
            AverageTimeinQueueList.Add(element);
        }

        public static void ResetStatistics()
        {
            NumberOfReceivedPackages = 0;

            NumberOfLostPackages = 0;

            NumberOfPackagesinQueue = 0;

            Time = 0;

            AverageTimeinQueueList = null;
            AverageTimeinQueueList = new List<double>();

            AverageTimeinQueue = 0;

            AveragePackageInQueueList = null;
            AveragePackageInQueueList = new Dictionary<int, double>();

            AveragePackageInQueue = 0;

            SimulationTime = 0;

            ServerLoad = 0;

            ServerLoadTime = 0;
        }

        public static void AddAveragePackageInQueue(int s, double time)
        {
            if (AveragePackageInQueueList.ContainsKey(s))
            {
                var actualTime = AveragePackageInQueueList[s];
                AveragePackageInQueueList[s] = (actualTime + time);
            }
            else
            {
                AveragePackageInQueueList.Add(s, time);
            }
        }

//--------------------------------------------------------Calculate-------------------------
        public static double CalculateAverageTime()
        {
            double sum = 0;
            double result;
            foreach (var i in AverageTimeinQueueList)
            {
                sum = sum + i;
            }

            result = sum / NumberOfPackagesinQueue;
            AverageTimeinQueue = result;
            return result;
        }

        public static double CalculateAveragePackageInQueue()
        {
            double sum = 0;


            foreach (var e in AveragePackageInQueueList)
            {
                sum = sum + e.Value * e.Key;
            }

            sum = sum / SimulationTime;
            AveragePackageInQueue = sum;
            return sum;
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

        public static void PrintAveragePackageInQueue()
        {
            Console.WriteLine($"[AveragePackageInQueue] {CalculateAveragePackageInQueue()}\n\n");
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
                NumberOfReceivedPackages = e.NumberOfRecivedPackage + NumberOfReceivedPackages;
                NumberOfLostPackages = e.NumberOfLostPackage + NumberOfLostPackages;
                NumberOfPackagesinQueue = e.NumberOfPackageinQueue + NumberOfPackagesinQueue;
                PackagesInSimulation = e.packagesInSimulation + PackagesInSimulation;
                AverageTimeinQueue = e.averageTimeinQueue + AverageTimeinQueue;
                AveragePackageInQueue = e.averagePackageInQueue + AveragePackageInQueue;
                SimulationTime = e.simulationTime + SimulationTime;
                ServerLoad = e.serverLoad + ServerLoad;
            }

            NumberOfPackagesinQueue = NumberOfPackagesinQueue / 100;
            PackagesInSimulation = PackagesInSimulation / 100;
            AverageTimeinQueue = AverageTimeinQueue / 100;
            AveragePackageInQueue = AveragePackageInQueue / 100;
            ServerLoad = ServerLoad / 100;
            PercentOfSuccess = (((float) Statistic.NumberOfReceivedPackages - (float) Statistic.NumberOfLostPackages) /
                (float) (Statistic.NumberOfReceivedPackages) * 100);
        }
    }
}