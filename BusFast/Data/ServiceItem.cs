﻿using BusFast.Models;
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
            Route = new RouteItem(service.Route);
        }

        public string Id { get; }
        public RouteItem Route { get; set; }
    }
}