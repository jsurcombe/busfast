using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Models
{
    public class Route
    {
        public Route(string name, Scrape.Route sRoute)
        {
            Name = name;
            Description = sRoute.Service_Name;

            var services = new List<Service>();

            foreach (var dkvp in sRoute.Timetable)
            {
                var direction = dkvp.Key;
                var directedRoute = dkvp.Value;

                var stops = directedRoute.Stops_List.Select(s => new Stop(s)).ToDictionary(s => s.Id);

                foreach (var service in directedRoute.Days.Where(ss => ss != null))
                {
                    foreach (var trip in service.Trips)
                        foreach (var day in trip.Days_Of_Operation.Where(kvp => kvp.Value == true).Select(kvp => kvp.Key))
                            services.Add(new Service(this, direction, day, trip, stops));
                }

            }

            Services = services.ToArray();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public Service[] Services { get; set; }
    }
}
