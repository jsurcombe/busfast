using BusFast.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Data
{
    public class ClusterItem
    {
        public ClusterItem(Cluster c)
        {
            Id = c.Id;
            Name = c.Name;
        }

        public string Id { get; }
        public string Name { get; }
    }
}
