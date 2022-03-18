using System;
using System.Collections.Generic;
using System.Text;

namespace MOPS.Tools
{
    public class GlobalStatistic
    {

        public int NumberOfRecivedPackage = 0;

        public int NumberOfLostPackage = 0;

        public int NumberOfPackageinQueue = 0;

        public int packagesInSimulation = 0;

        public float averageTimeinQueue = 0;

        public float averagePackageInQueue = 0;

        public float simulationTime = 0;

        public float serverLoad = 0;

        public float programTime = 0;
        public float percentOfSuccess = 0;

        public GlobalStatistic()
        {
            this.NumberOfRecivedPackage = Statistic.NumberOfRecivedPackage;
            this.NumberOfLostPackage = Statistic.NumberOfLostPackage;
            this.NumberOfPackageinQueue = Statistic.NumberOfPackageinQueue;
            this.packagesInSimulation = Statistic.packagesInSimulation;
            this.averageTimeinQueue = Statistic.averageTimeinQueue;
            this.averagePackageInQueue = Statistic.averagePackageInQueue;
            this.simulationTime = Statistic.simulationTime;
            this.serverLoad = Statistic.serverLoad;
            this.programTime = Statistic.ProgramTime;
            this.percentOfSuccess = Statistic.percentOfSuccess;
        }





    }
}
