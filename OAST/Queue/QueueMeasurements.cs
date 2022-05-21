using System;
using System.Collections.Generic;

namespace OAST.Queue
{
    public class QueueMeasurements : IQueueMeasurements
    {
        public double TimeInQueue { get; set; }
        public double AverageNumberOfPackagesInQueue { get; set; }
        public int NumberOfPackagesThatWereQueue { get; set; }
        public  int NumberOfPackagesThatWereNotQueue { get; set; }
        
        public void Reset()
        {
            TimeInQueue = 0;
            AverageNumberOfPackagesInQueue = 0;
            NumberOfPackagesThatWereQueue = 0;
            NumberOfPackagesThatWereNotQueue = 0;
        }

        public void AddTimeInQueue(double time)
        {
            TimeInQueue = TimeInQueue + time;
        }

        public double CalculateAverageTime()
        {
            return   TimeInQueue / NumberOfPackagesThatWereQueue;
        }

        public void IncrementNumberOfPackagesThatWereInQueue()
        {
            NumberOfPackagesThatWereQueue += 1;
        }
        
        public void PrintAverageTimeInQueue()
        {
            Console.WriteLine($"[QUEUE] {TimeInQueue} \n Number of messages that were in queue: {NumberOfPackagesThatWereQueue}\n Number of messages that weren't in queue: {NumberOfPackagesThatWereNotQueue}\n Average Time in Queue: {CalculateAverageTime()}\n");
        }

        public void IncrementNumberOfPackagesThatWereNotInQueue()
        {
            NumberOfPackagesThatWereNotQueue += 1;
            
        }
    }
}