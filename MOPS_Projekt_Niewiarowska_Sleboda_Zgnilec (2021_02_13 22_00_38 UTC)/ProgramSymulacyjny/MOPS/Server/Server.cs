using MOPS.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOPS
{
   public class Server
    {
        public bool bussy { get; set; }
        public int bitRate { get; set; }
        public float bussyStart { set; get; }
        public float bussyStop { set; get; }


        public Server()
        {

        }

        public Server(int bitRate)
        {
            this.bussy = false;
            this.bitRate = bitRate;
        }


        public void setBussy()
        {
            bussy = true;
            bussyStart = Statistic.Time;
              
        }

        public void setAvailable()
        {
            bussy = false;
            bussyStop = Statistic.Time;
            Statistic.calculateServerLoadTime(bussyStart, bussyStop);
        }






       


    }
}
