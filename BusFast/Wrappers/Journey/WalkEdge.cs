using BusFast.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Wrappers.Journey
{
    public class WalkEdge : Edge
    {
        private readonly Stop _stop;

        public WalkEdge(DateTime at, TimeSpan distance, StopNode to) : base(at, (float)distance.TotalMinutes, to)
        {
            _stop = to.Stop;
        }

        public override string Describe()
        {
            return $"Walk to {_stop.Name}";
        }
    }
}
