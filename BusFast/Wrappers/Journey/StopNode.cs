using BusFast.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Wrappers.Journey
{
    public class StopNode : Node
    {
        private readonly Stop _stop;

        public StopNode(DateTime at, Stop stop) : base(at) { _stop = stop; }
        public override IEnumerable<Edge> Edges => throw new NotImplementedException();

        public override int GetHashCode()
        {
            return _stop.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is StopNode ass && ass._stop == _stop;
        }
    }
}
