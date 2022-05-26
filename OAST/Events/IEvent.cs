namespace OAST.Events
{
    public interface IEvent
    {
        EventType Type { get; set; }
        double Time { get; set; }
        int EventId { get; set; } 
    }
}