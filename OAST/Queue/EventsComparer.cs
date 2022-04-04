using System.Collections.Generic;
using OAST.Events;

namespace OAST.Queue
{
    public class EventsComparer : IComparer<Event>
    {
        public int Compare(Event x, Event y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (ReferenceEquals(null, y)) return 1;
            if (ReferenceEquals(null, x)) return -1;
            
            return x.Time.CompareTo(y.Time);
        }
    }
}