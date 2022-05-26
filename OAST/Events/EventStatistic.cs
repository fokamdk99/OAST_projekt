namespace OAST.Events
{
    public class EventStatistic
    {
        public int Id { get; set; }
        public bool? Blocked { get; set; }
        public double? ArrivalTime { get; set; }
        public double? ServiceStartTime { get; set; }
        public double? DepartureTime { get; set; }
        public int? FillOnArrival { get; set; }

        public EventStatistic()
        {
            Blocked = null;
            ArrivalTime = null;
            ServiceStartTime = null;
            DepartureTime = null;
            FillOnArrival = null;
        }

        public EventStatistic(int id)
        {
            Id = id;
        }

        public EventStatistic(int id,
            bool blocked,
            double arrivalTime,
            double serviceStartTime,
            double departureTime,
            int fillOnArrival)
        {
            Id = id;
            Blocked = blocked;
            ArrivalTime = arrivalTime;
            ServiceStartTime = serviceStartTime;
            DepartureTime = departureTime;
            FillOnArrival = fillOnArrival;
        }
    }
}