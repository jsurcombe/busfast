using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Models
{
    public class Service
    {
        public DaysCode Days { get; set; }
        public ServiceStop[] Stops { get; set; }
    }
}
