using BusFast.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Wrappers.Journey
{
    public abstract class Node
    {
        protected readonly DataService _ds;

        protected Node(DataService ds)
        {
            _ds = ds;
        }

        public abstract IEnumerable<Edge> Edges(DateTime at);
    }
}
