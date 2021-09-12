using BusFast.Data;
using BusFast.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Controllers
{
    [ApiController]
    [Route("api/stops")]
    public class StopController
    {
        private readonly DataService _ds;

        public StopController(DataService ds)
        {
            _ds = ds;
        }

        [HttpGet]
        public Stop[] Search(string q)
        {
            if (string.IsNullOrEmpty(q))
                return new Stop[] { };
            else
                return _ds.Stops.Where(si => si.Name.Contains(q, StringComparison.CurrentCultureIgnoreCase)).Take(10).ToArray();
        }

        [HttpGet]
        [Route("{id}")]
        public StopView View(int id)
        {
            var stop = _ds.GetStop(id);

            var res = new StopView() { Name = stop.Name };
            return res;
        }
    }
}
