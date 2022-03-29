﻿using System;

namespace OAST
{
    public static class Parameters
    {
        public static int numberOfSources = 0;
        public static int numberOfPackages = 0; 
        public static int queueSize = 0;
        public static int mi = 0;
        public static int lambda = 0;

        public static void PrintMainParameters()
        {
            Console.WriteLine($"\n\n[PARAMETERS]\nNumber of sources: {numberOfSources}\n number of packages: {numberOfPackages}\n" +
                $"Queue size: {queueSize}\nMi: {mi}\nLambda: {lambda}");

        }
    }
}
