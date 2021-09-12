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
            this.Name = route.Name;
        }

        public string Name { get; }
    }
}
