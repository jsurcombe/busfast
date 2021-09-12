using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Models
{
    public class ServiceStop
    {
        public Stop Stop { get; set; }
        public TimeSpan Time { get; set; }
    }
}
