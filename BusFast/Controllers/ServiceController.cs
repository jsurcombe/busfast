using BusFast.Data;
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

            var iterators = new SimplePriorityQueue<IEnumerator<ServiceStopHelper.Occurrence>>();
            var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TZConvert.GetTimeZoneInfo("Europe/London"));

            foreach (var s in services)
            {
                var sh = new ServiceStopHelper(s);
                var occ = sh.Occurrences(now).GetEnumerator();
                occ.MoveNext();

                iterators.Enqueue(occ, (float)(occ.Current.At - DateTime.Now).TotalDays);
            }

            var res = new List<ServiceUpcoming>();

            while (res.Count < 10)
            {
                var n = iterators.Dequeue();

                res.Add(new ServiceUpcoming()
                {
                    Service = new ServiceItem(n.Current.ServiceStop.Service),
                    At = n.Current.At
                });

                n.MoveNext();
                iterators.Enqueue(n, (float)(n.Current.At - DateTime.Now).TotalDays);
            }

            return res.ToArray();
        }
    }
}
