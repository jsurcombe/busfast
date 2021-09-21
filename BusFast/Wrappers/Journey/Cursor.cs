using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Wrappers.Journey
{
    public class Cursor
    {
        public readonly DateTime Start;
        public readonly Cursor Previous;
        public readonly Edge Edge;
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

        public Cursor(Cursor previous, Edge edge)
        {
            Start = previous.Start;
            Previous = previous;
            Edge = edge;
            Cost = previous.Cost + edge.Cost;
            Node = edge.To;
            At = edge.At;
        }

        public float Priority
        {
            get
            {
                return (float)((At - Start).TotalMinutes) + Cost;
            }
        }

        public IEnumerable<Edge> Edges => Node.Edges(At);
    }
}
