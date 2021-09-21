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

        public ClusterNode(Cluster cluster, DataService ds) : base(ds)
        {
            _cluster = cluster;
        }

        public override IEnumerable<Edge> Edges(DateTime at)
        {
            // be standing at any stop in this cluster
            foreach (var s in _cluster.Stops)
                yield return new WaitEdge(at, 0f, new StopNode(s, _ds));
        }

        public override int GetHashCode()
        {
            return _cluster.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is ClusterNode cn && cn._cluster == _cluster;
        }
    }
}
