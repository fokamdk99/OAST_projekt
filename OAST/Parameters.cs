using System;
using System.Collections.Generic;
using System.Text;

namespace OAST
{
    public static class Parameters
    {
        public static int numberOfSources = 0; 
        public static int peakRate = 0; 
        public static int packageSize = 0;
        public static int numberOfPackages = 0; 
        public static int queueSize = 0; 
        public static int serverBitRate = 0;
        public static float serverTime = 0;
        public static float timeBetweenPackages = 0; 

        //----------------------------------------Calculate parameters----------------------------------------------

        public static void CalculateTimeBetweenPackages()
        {
            float size = packageSize;
            float rate = peakRate;
            timeBetweenPackages = size / rate;
        }

        //---------------------------------------Print Parameters----------------------------------------------------
        public static void PrintMainParameters()
        {
            Console.WriteLine($"\n\n[PARAMETERS]\nNumber of sources: {numberOfSources}\n number of packages: {numberOfPackages}\n" +
                $"Queue size: {queueSize}\nServer working time: {serverTime}\nTime between packages: {timeBetweenPackages}");

        }
    }
}
