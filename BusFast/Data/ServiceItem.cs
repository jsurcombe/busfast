using BusFast.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Data
{
    public class ServiceItem
    {
        public ServiceItem(Service service)
        {
            Id = service.Id;
            RouteName = service.Route.Name;
        }

        public string Id { get; }
        public string RouteName { get; set; }
    }
}
