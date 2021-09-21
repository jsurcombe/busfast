using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Wrappers.Journey
{
    public class WaitEdge : Edge // an edge which represents just doing nothing (standing still or staying on a bus)
    {
        public WaitEdge(DateTime at, float cost, Node to) : base(at, cost, to)
        {
        }

        public override string Describe()
        {
            return null;
        }
    }
}
