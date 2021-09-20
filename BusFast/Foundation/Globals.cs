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
    }
}
