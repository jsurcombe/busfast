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
            Description = service.Route.Description;
        }

        public string Id { get; }
        public string RouteName { get; }
        public string Description { get; }
    }
}
