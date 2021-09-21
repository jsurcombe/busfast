using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Wrappers.Journey
{
    public abstract class Edge
    {
        public Edge(DateTime at, float cost, Node to)
        {
            At = at;
            Cost = cost;
            To = to;
        }

        public readonly DateTime At;
        public readonly float Cost;
        public readonly Node To;

        public abstract string Describe();
    }
}
