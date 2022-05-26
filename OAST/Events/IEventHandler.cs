using OAST.Tools;

namespace OAST.Events
{
    public interface IEventHandler
    {
        void HandleEvent(Event @event, ref Statistic statistic);
        void HandleComingEvent(Event @event, ref Statistic statistic);
        void HandleFinishEvent(Event @event, ref Statistic statistic);
    }
}