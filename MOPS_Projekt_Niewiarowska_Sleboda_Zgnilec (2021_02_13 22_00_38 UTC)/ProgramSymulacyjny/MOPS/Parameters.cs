using System;
using System.Collections.Generic;
using System.Text;

namespace MOPS
{
    public static class Parameters
    {
        public static String SourceType = "";
        public static int numberOfSources = 0; 
        public static int peakRate = 0; 
        public static int packageSize = 0; 
        public static int OFFtime = 0;
        public static int ONtime = 0;
        public static int numberOfPackages = 0; 
        public static int queueSize = 0; 
        public static int serverBitRate = 0;
        public static float serverTime = 0;
        public static float timeBetweenPackages = 0; 

        //----------------------------------------Calculate parameters----------------------------------------------
        public static void CalculateServerTime()
        {
            float s = packageSize;
            float b = serverBitRate;
            serverTime = s / b;
        }

        public static void CalculateTimeBetweenPackages()
        {
            float size = packageSize;
            float rate = peakRate;
            timeBetweenPackages = size / rate;
        }



        //---------------------------------------Print Parameters----------------------------------------------------
        public static void PrintAllParameters()
        {
            Console.WriteLine($"[PARAMETERS]\nNumber of sources: {numberOfSources}\n peakRate: {peakRate}\nPackage Size: {packageSize} \nOFF time: {OFFtime}\n number of packages: {numberOfPackages}\n" +
                $"Queue size: {queueSize}\nServer BitRate: {serverBitRate}\nServer Time: {serverTime}\nTime between packages: {timeBetweenPackages}");

        }

        public static void PrintMainParameters()
        {
            Console.WriteLine($"\n\n[PARAMETERS]\nNumber of sources: {numberOfSources}\n number of packages: {numberOfPackages}\n" +
                $"Queue size: {queueSize}\nServer working time: {serverTime}\nTime between packages: {timeBetweenPackages}");

        }
    }
}
