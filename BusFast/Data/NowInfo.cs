using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Data
{
    public class NowInfo
    {
        public DateTime Now { get; set; }
        public DateTime NowPlusOneMinute { get; set; }
        public int DayOfWeek { get; set; }
    }
}
