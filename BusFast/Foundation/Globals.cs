using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeZoneConverter;

namespace BusFast.Foundation
{
    public static class Globals
    {
        public static DateTime GuernseyNow =>
            TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TZConvert.GetTimeZoneInfo("Europe/London"));

        internal static DateTime GetTime(string spec)
        {
            var r = GuernseyNow;
            // remove the time
            r -= r.TimeOfDay;
            // add days
            var ss = spec.Split("|");
            var dayOffset = int.Parse(ss[0]);
            var time = TimeSpan.Parse(ss[1]);

            return r.AddDays(dayOffset).Add(time);

        }
    }
}
