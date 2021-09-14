using BusFast.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Data
{
    public class RouteItem
    {

        public RouteItem(Route route)
        {
            Name = route.Name;
            Description = route.Description;
        }

        public string Name { get; }
        public string Description { get; }
    }
}
