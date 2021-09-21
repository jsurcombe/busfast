using BusFast.Data;
using BusFast.Foundation;
using BusFast.Models;
using BusFast.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using TimeZoneConverter;

namespace BusFast.Controllers
{
    [ApiController]
    [Route("api/services")]
    public class ServiceController
    {
        private readonly DataService _ds;

        public ServiceController(DataService ds)
        {
            _ds = ds;
        }

        [HttpGet]
        [Route("{id}")]
        public ServiceItem View(string id)
        {
            return new ServiceItem(_ds.GetService(id));
        }

        [HttpGet]
        [Route("{id}/stops")]
        public ServiceStopItem[] Stops(string id)
        {
            return _ds.GetService(id).Stops.Select(ss => new ServiceStopItem(ss)).ToArray();
        }

        [HttpGet]
        [Route("upcoming")]
        public ServiceUpcoming[] Upcoming(string clusterId)
        {
            var services = _ds.GetServicesAt(clusterId);

            return ServiceStopHelper.Occurrences(services, Globals.GuernseyNow).Take(10).Select(ss => new ServiceUpcoming(ss)).ToArray();
        }
    }
}
