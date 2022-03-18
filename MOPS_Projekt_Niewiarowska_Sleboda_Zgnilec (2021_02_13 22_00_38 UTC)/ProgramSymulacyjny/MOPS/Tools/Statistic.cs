using System;
using System.Collections.Generic;
using System.Text;

namespace MOPS.Tools
{
    public static class Statistic
    {


        public static int NumberOfRecivedPackage = 0;

        public static int NumberOfLostPackage = 0;

        public static int NumberOfPackageinQueue = 0;

        public static float Time = 0;

        public static int packagesInSimulation = 0;

        public static int packageSize = 0;

        private static List<float> averageTimeinQueueList = new List<float>();

        public static float averageTimeinQueue = 0;

        private static Dictionary<int,float> averagePackageInQueueList = new Dictionary<int,float>();

        public static float averagePackageInQueue = 0;

        public static float simulationTime = 0;

        public static float serverLoad = 0;

        public static float serverLoadTime = 0;

        public static List<GlobalStatistic> globalList = new List<GlobalStatistic>();

        public static float ProgramTime = 0;
        public static float percentOfSuccess = 0;

        // -------------------------------------Inceremnt--------------------------------
        public static void incrementRecivedPackage()
        {
            NumberOfRecivedPackage = NumberOfRecivedPackage + 1;
        }

        public static void incrementLostPackage()
        {
            NumberOfLostPackage = NumberOfLostPackage + 1;
        }

        public static void incrementPackageInQueue()
        {
            NumberOfPackageinQueue = NumberOfPackageinQueue + 1;
        }

        public static void incrementTime(float deltaT)
        {
            Time = Time + deltaT;
        }

        //--------------------------------------------Decrement-------------------------------------
        public static void decrementRecivedPackage()
        {
            NumberOfRecivedPackage = NumberOfRecivedPackage - 1;
        }

        public static void decrementLostPackage()
        {
            NumberOfLostPackage = NumberOfLostPackage - 1;
        }

        public static void decrementPackageInQueue()
        {
            NumberOfPackageinQueue = NumberOfPackageinQueue - 1;
        }

        //----------------------------------------Reset------------------------------------------
        public static void resetPackageInQueue()
        {
            NumberOfPackageinQueue = 0;
        }

        public static void resetRecivedPackage()
        {
            NumberOfRecivedPackage = 0;
        }

        public static void resetLostPackage()
        {
            NumberOfLostPackage =0;
        }

        public static void addAverageTimeinQueue(float element)
        {
            averageTimeinQueueList.Add(element);
        }

        public static void RESETSTATISTIC()
        {
        NumberOfRecivedPackage = 0;

        NumberOfLostPackage = 0;

        NumberOfPackageinQueue = 0;

        Time = 0;
 
        packageSize = 0;

        averageTimeinQueueList = null;
        averageTimeinQueueList = new List<float>();

        averageTimeinQueue = 0;

        averagePackageInQueueList = null;
        averagePackageInQueueList = new Dictionary<int, float>();

        averagePackageInQueue = 0;

        simulationTime = 0;

        serverLoad = 0;

        serverLoadTime = 0;
    }

        public static void addAveragePackageInQueue(int s, float time)
        {
            if (averagePackageInQueueList.ContainsKey(s))
            {
                var actualTime = averagePackageInQueueList[s];
                averagePackageInQueueList[s] = (actualTime + time);
            }
            else
            {
                averagePackageInQueueList.Add(s, time);
            }
        }
//--------------------------------------------------------Calculate-------------------------
        public static float calculateAverageTime()
        {
            float sum = 0;
            float result;
            foreach (var i in averageTimeinQueueList)
            {
                sum = sum + i;
            }
            result = sum / NumberOfPackageinQueue;
            averageTimeinQueue = result;
            return result;
        }

        public static float calculateAveragePackageInQueue()
        {
            float sum = 0;
            

            foreach (var e in averagePackageInQueueList)
            {
                sum = sum + e.Value * e.Key;
            }
            sum = sum / simulationTime;
            averagePackageInQueue = sum;
            return sum;
        }


        public static float calculateServerLoad()
        {
            serverLoad = serverLoadTime / Statistic.simulationTime;
            return serverLoad;
        }

        public static void calculateServerLoadTime(float bussyStart, float bussyStop)
        {
            serverLoadTime = serverLoadTime + (bussyStop - bussyStart);
           
        }

        //-------------------------------------Print----------------------------------------

        public static void printStatistic()
        {
            Console.WriteLine($"[STATISTIC]\nNumberOfRecivedPackage: {NumberOfRecivedPackage}\nNumberOfLostPackage: {NumberOfLostPackage}\n\n");

        }

        public static void printAverageTimeInQueue()
        {
            Console.WriteLine($"\nAverage Time in Queue: {calculateAverageTime()}\n\n");

        }

        public static void printAveragePackageInQueue()
        {
            
            Console.WriteLine($"[AveragePackageInQueue] {calculateAveragePackageInQueue()}\n\n");

        }


        public static void printServerLoad()
        {

            Console.WriteLine($"[Server Load] {calculateServerLoad()}\n\n");

        }

        //-----------------------------GlobalList----------------------------

        public static void calculate()
        {
            foreach (var e in globalList)
            {
                NumberOfRecivedPackage = e.NumberOfRecivedPackage + NumberOfRecivedPackage;
                NumberOfLostPackage = e.NumberOfLostPackage + NumberOfLostPackage;
                NumberOfPackageinQueue = e.NumberOfPackageinQueue + NumberOfPackageinQueue;
                packagesInSimulation = e.packagesInSimulation + packagesInSimulation;
                averageTimeinQueue = e.averageTimeinQueue + averageTimeinQueue;
                averagePackageInQueue = e.averagePackageInQueue + averagePackageInQueue;
                simulationTime = e.simulationTime + simulationTime;
                serverLoad = e.serverLoad + serverLoad;

            }
                NumberOfRecivedPackage =  NumberOfRecivedPackage;
                NumberOfLostPackage = NumberOfLostPackage;
                NumberOfPackageinQueue =  NumberOfPackageinQueue / 100;
                packagesInSimulation =  packagesInSimulation / 100;
                averageTimeinQueue = averageTimeinQueue / 100;
                averagePackageInQueue = averagePackageInQueue / 100;
                simulationTime = simulationTime;
                serverLoad =  serverLoad / 100;
                percentOfSuccess = (((float)Statistic.NumberOfRecivedPackage - (float)Statistic.NumberOfLostPackage) / (float)(Statistic.NumberOfRecivedPackage) * 100);


        }

    }
}
