using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Models
{
    public class Route
    {
        public string Name { get; set; }
        public Service[] Services { get; set; }
    }
}
