using System;

namespace OAST.Queue
{
    public class QueueMeasurements : IQueueMeasurements
    {
        public double TimeinQueue { get; set; }
        public double AverageNumberOfPackagesInQueue { get; set; }
        public int NumberOfPackagesThatWereQueue { get; set; } // wtf to jakis licznik zwiekszajacy sie przez caly czas trwania symulacji?
        public  int NumberOfPackagesThatWereNotQueue { get; set; }
        public void Reset()
        {
            TimeinQueue = 0;
            AverageNumberOfPackagesInQueue = 0;
            NumberOfPackagesThatWereQueue = 0;
            NumberOfPackagesThatWereNotQueue = 0;
        }

        public void AddTimeInQueue(double time)
        {
            TimeinQueue += time;
        }

        public double CalculateAverageTime()
        {
            return   TimeinQueue / NumberOfPackagesThatWereQueue;
        }

        public void IncrementNumberOfPackagesThatWereInQueue()
        {
            NumberOfPackagesThatWereQueue += 1;
        }
        
        public void PrintAverageTimeInQueue()
        {
            Console.WriteLine($"[QUEUE] \n Number of messages that were in queue: {NumberOfPackagesThatWereQueue}\n Number of messages that weren't in queue: {NumberOfPackagesThatWereNotQueue}\n Average Time in Queue: {CalculateAverageTime()}\n");
        }

        public void IncrementNumberOfPackagesThatWereNotInQueue()
        {
            NumberOfPackagesThatWereNotQueue += 1;
            
        }
    }
}