using BusFast.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Wrappers.Journey
{
    // represents standing at a cluster
    public class ClusterNode : Node
    {
        private readonly Cluster _cluster;

        public ClusterNode(Cluster cluster)
        {
            _cluster = cluster;
        }

        public override IEnumerable<Edge> Edges(Cursor c)
        {
            get
            {
                // be standing at any stop in this cluster
                foreach (var s in _cluster.Stops)
                    yield return new Edge(c.At, 0.0, new StopNode(s));
            }
        }

        public override int GetHashCode()
        {
            return _cluster.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is AtClusterState acs && acs._cluster == _cluster;
        }
    }
}
