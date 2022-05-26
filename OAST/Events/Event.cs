namespace OAST.Events
{
   public class Event : IEvent
    {
        public EventType Type { get; set; }
        public double Time { get; set; }
        public int EventId { get; set; } 
       

        public Event()
        {}

        public Event(EventType type, double time)
        {
            Type = type;
            Time = time;
            
        }
    }
}
