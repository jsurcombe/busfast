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

        public DaysCode Days { get; set; }
        public List<ServiceStop> Stops { get; set; }
        public Route Route { get; }
        public string Id { get; set; }
    }
}
