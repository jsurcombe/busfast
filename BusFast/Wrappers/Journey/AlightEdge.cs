using BusFast.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Wrappers.Journey
{
    public class AlightEdge : Edge
    {
        private readonly Stop _stop;

        public AlightEdge(DateTime at, StopNode to) : base(at, 0f, to)
        {
            _stop = to.Stop;
        }

        public override string Describe()
        {
            return $"Alight at {_stop.Cluster.Name}";
        }
    }
}
