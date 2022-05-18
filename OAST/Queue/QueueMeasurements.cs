using System;

namespace OAST.Queue
{
    public class QueueMeasurements : IQueueMeasurements
    {
        public double AverageTimeinQueue { get; set; }
        public double AverageNumberOfPackagesInQueue { get; set; }
        public int NumberOfPackagesInQueue { get; set; } // wtf to jakis licznik zwiekszajacy sie przez caly czas trwania symulacji?
        
        public void Reset()
        {
            AverageTimeinQueue = 0;
            AverageNumberOfPackagesInQueue = 0;
            NumberOfPackagesInQueue = 0;
        }

        public void AddAverageTimeinQueue(double time)
        {
            AverageTimeinQueue += time;
        }
        
        public double CalculateAverageTime()
        {
            double sum = AverageTimeinQueue;
            double result;

            result = sum / NumberOfPackagesInQueue;
            AverageTimeinQueue = result;
            return result;
        }
        
        public void IncrementNumberOfPackagesInQueue()
        {
            NumberOfPackagesInQueue += 1;
        }
        
        public void PrintAverageTimeInQueue()
        {
            Console.WriteLine($"[QUEUE] \n Number of messages that were in queue: {NumberOfPackagesInQueue}\n Average Time in Queue: {CalculateAverageTime()}\n");
        }
    }
}