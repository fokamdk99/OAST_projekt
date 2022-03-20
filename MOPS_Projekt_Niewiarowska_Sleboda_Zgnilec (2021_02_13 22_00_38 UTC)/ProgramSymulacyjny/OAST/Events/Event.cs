using System;
using OAST.Packages;

namespace OAST.Events
{
   public class Event
    {
        public String type { get; set; }
        public float time { get; set; }
        public int sourceID { get; set; }
        public Event()
        {}

        public Event(int id, String type, float time)
        {
            this.sourceID = id;
            this.type = type;
            this.time = time;
        }

        public Package createPackage(int id)
        {
            Package package = new Package(id, this.sourceID, Parameters.packageSize, time);
            return package;
        }
        
    }
}
