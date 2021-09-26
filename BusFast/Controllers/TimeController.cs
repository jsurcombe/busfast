using BusFast.Data;
using BusFast.Foundation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Controllers
{
    [Route("api/time")]
    public class TimeController
    {
        [HttpGet]
        public NowInfo GetNowInfo()
        {
            // get info about the current time and day in Guernsey
            var d = Globals.GuernseyNow;

            return new NowInfo()
            {
                DayOfWeek = (int)d.DayOfWeek,
                Now = d,
                NowPlusOneMinute = d.AddMinutes(1)
            };
        }
    }
}
