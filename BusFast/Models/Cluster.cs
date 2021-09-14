using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Models
{
    public class Cluster
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string[] Words { get; set; }
        public Stop[] Stops { get; set; }
        public string[] Tokens { get; set; }
    }
}
