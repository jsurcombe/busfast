using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Models
{
    public class Service
    {
        public Service(Route route)
        {
            Route = route;
        }

        public Service(Route route, Direction direction, Scrape.Day day, Scrape.Trip trip, Dictionary<int, Stop> stops) : this(route)
        {
            Route = route;
            Direction = direction;
            DayOfWeek = (DayOfWeek)day;
            Id = $"{trip.Trip_Id}-{day}";

            Stops = new();
            ServiceStop? previous = null;

            foreach (var s in trip.Stops.OrderBy(si => si.Time))
            {
                var ss = new ServiceStop(this, s, stops, previous);
                Stops.Add(ss);
                previous = ss;
            }

        }

        public Direction Direction { get; }
        public DayOfWeek DayOfWeek { get; }
        public List<ServiceStop> Stops { get; }
        public Route Route { get; }
        public string Id { get; }

        public Service Return { get; internal set; }
    }
}
