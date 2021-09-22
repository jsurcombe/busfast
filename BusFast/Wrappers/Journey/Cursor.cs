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

        public Cursor(DateTime at, Node node)
        {
            Start = at;
            At = at;
            Node = node;
            Edge = new WaitEdge(at, 0f, node);
        }

        public Cursor(Cursor previous, Edge edge)
        {
            Start = previous.Start;
            Previous = previous;
            Edge = edge;
            if (edge.Cost < 0f)
                throw new NotImplementedException();
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

        public IEnumerable<Cursor> Cursors
        {
            get
            {
                var l = new List<Cursor>();

                var c = this;
                l.Add(c);

                while (c.Previous != null)
                {
                    c = c.Previous;
                    l.Add(c);
                }

                for (int i = l.Count - 1; i >= 0; i--)
                    yield return l[i];
            }
        }
    }
}
