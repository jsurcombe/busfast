using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Wrappers.Journey
{
    public abstract class Node
    {
        public readonly DateTime At;

        public Node(DateTime at) { At = at; }

        public abstract IEnumerable<Node> Edges { get; }
    }
}
