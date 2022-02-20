using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Models
{
    public class ServiceStop
    {
        public ServiceStop(Service service, Scrape.TripStop s, Dictionary<int, Stop> stops, ServiceStop? previous)
        {
            Service = service;
            Stop = stops[s.Stop_Id];
            Time = s.Time;

            if (previous != null)
                previous.Next = this;
        }

        public Stop Stop { get; set; }
        public TimeSpan Time { get; set; }
        public Service Service { get; }

        // link to the next ServiceStop
        public ServiceStop? Next { get; set; }
    }
}
