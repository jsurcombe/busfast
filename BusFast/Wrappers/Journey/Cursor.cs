using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Wrappers.Journey
{
    public class Cursor
    {
        public readonly DateTime Start;
        public readonly DateTime At;
        public readonly float Cost;
        public readonly Node Node;

        public Cursor(DateTime at, float cost, Node node)
        {
            Start = at;
            At = at;
            Cost = cost;
            Node = node;
        }

        public float Priority
        {
            get
            {
                return (float)((At - Start).TotalMinutes) + Cost;
            }
        }
    }
}
