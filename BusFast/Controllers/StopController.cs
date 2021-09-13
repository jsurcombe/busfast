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
        public StopItem[] Search(string q)
        {
            if (string.IsNullOrEmpty(q))
                return new StopItem[] { };
            else
                return _ds.Stops.Where(si => si.Name.Contains(q, StringComparison.CurrentCultureIgnoreCase)).Take(10).Select(s => new StopItem(s)).ToArray();
        }

        [HttpGet]
        [Route("{id}")]
        public StopItem View(int id) => new StopItem( _ds.GetStop(id));
    }
}
