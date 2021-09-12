using BusFast.Data;
using BusFast.Models;
using BusFast.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Priority_Queue;
using System;
using System.Collections.Generic;

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
        [Route("upcoming")]
        public ServiceUpcoming[] Upcoming(int stopId)
        {
            var services = _ds.GetServicesAt(stopId);

            var iterators = new SimplePriorityQueue<IEnumerator<ServiceStopHelper.Occurrence>>();

            var now = DateTime.Now;

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

                res.Add(new ServiceUpcoming() { Route = new RouteItem(n.Current.ServiceStop.Service.Route), At = n.Current.At });

                n.MoveNext();
                iterators.Enqueue(n, (float)(n.Current.At - DateTime.Now).TotalDays);
            }

            return res.ToArray();
        }
    }
}
