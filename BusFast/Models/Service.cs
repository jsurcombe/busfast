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

        public Direction Direction { get; set; }
        public Days Days { get; set; }
        public List<ServiceStop> Stops { get; set; }
        public Route Route { get; }
        public string Id { get; set; }

        public string Description
        {
            get
            {
                if (Direction == Direction.Outbound)
                    return Route.Description;
                else // reverse the description
                    return string.Join(" - ", Route.Description.Split('-').Select(s => s.Trim()).Reverse());
            }
        }
    }
}
