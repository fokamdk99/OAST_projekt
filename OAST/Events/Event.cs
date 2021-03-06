using OAST.OASTPackages;

namespace OAST.Events
{
   public class Event : IEvent
    {
        public EventType Type { get; set; }
        public double Time { get; set; }
        public int SourceId { get; set; } 
       

        public Event()
        {}

        public Event(int id, EventType type, double time)
        {
            SourceId = id;
            Type = type;
            Time = time;
            
        }

        public Package CreatePackage(int id)
        {
            Package package = new Package(id, SourceId, Time);

            return package;
        }


    }
}
