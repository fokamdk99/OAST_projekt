using System;

namespace OAST
{
    public interface IParameters
    {
        public double Lambda { get; set; }
        public double Mi { get; set; }
        public int QueueSize { get; set; }
        public int SimulationTime { get; set; }
        public int BlockSize { get; set; }
        public int NumberOfSimulations { get; set; }
        public double CutOffWaitingTimeDiff { get; set; }
    }
    
    public class Parameters : IParameters
    {
        public double Lambda { get; set; } = 0;
        public double Mi { get; set; } = 0;
        public int QueueSize { get; set; } = 0;
        public int SimulationTime { get; set; } = 0;
        public int BlockSize { get; set; } = 0;
        public int NumberOfSimulations { get; set; } = 0;
        public double CutOffWaitingTimeDiff { get; set; } = 0.3;

        public Parameters()
        {
            
        }

        public void PrintMainParameters()
        {
            Console.WriteLine($"\n\n[PARAMETERS]\n number of packages: {SimulationTime}\n" +
                $"Queue size: {QueueSize}\nMi: {Mi}\nLambda: {Lambda}");

        }
        
        // TODO: jak obliczyć charakterystykę rozkładu prawdopodobieństwa między wyjściami klientów dla obciążenia systemu jak w punkcie I
        // TODO: utworzyc eventy o rozkładzie jednostajnym ze średnią i wariancją taką samą, jak w części I
        // TODO: usuwanie wplywu warunkow poczatkowych
    }
}
