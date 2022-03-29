using System;
using System.Collections.Generic;

namespace OAST.Tools
{
    public class Statistic : IStatistic
    {
        public int NumberOfReceivedPackages { get; set; } // wtf czemu ma sluzyc ta zmienna, dlaczego od razu zwiekszamy o jeden gdy przychodzi pakiet
        // zanim w ogole sprawdzimy czy jest miejsce w kolejce

        public int NumberOfLostPackages { get; set; }

        public double Time { get; set; }

        public double SimulationTime { get; set; }

        public double PercentOfSuccess { get; set; }

        // -------------------------------------Inceremnt--------------------------------
        public void IncrementNumberOfReceivedPackages()
        {
            NumberOfReceivedPackages += 1;
        }

        public void IncrementNumberOfLostPackages()
        {
            NumberOfLostPackages += 1;
        }

        public void ResetStatistics()
        {
            NumberOfReceivedPackages = 0;
            NumberOfLostPackages = 0;
            Time = 0;
            SimulationTime = 0;
        }

        public void PrintStatistics()
        {
            Console.WriteLine(
                $"[STATISTIC]\nNumberOfRecivedPackage: {NumberOfReceivedPackages}\nNumberOfLostPackage: {NumberOfLostPackages}\n\n");
        }

        //-----------------------------GlobalList----------------------------

        
    }
}