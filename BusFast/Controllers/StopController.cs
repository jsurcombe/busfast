using BusFast.Data;
using BusFast.Foundation;
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
    }
}
