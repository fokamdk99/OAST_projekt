namespace MOPS.Events
{
    public interface IEventHandler
    {
        void HandleEvent(Event @event, int eventId);
        void HandleComingEvent(Event @event, int eventId);
        void HandleFinishEvent(int eventId);
    }
}