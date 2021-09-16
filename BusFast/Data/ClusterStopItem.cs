using BusFast.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Data
{
    public class ClusterStopItem
    {
        public ClusterStopItem(Stop s)
        {
            Id = s.Id;
            Bound = s.Bound;
        }

        public int Id { get; }
        public string Bound { get; }
    }
}
