using BusFast.Foundation;
using BusFast.Models;
using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Wrappers.Journey
{
    public class StopNode : Node
    {
        private readonly Stop _stop;

        public StopNode(Stop stop, DataService ds) : base(ds) { _stop = stop; }

        public override IEnumerable<Edge> Edges(DateTime at)
        {
            // get an iterator for services leaving here after now
            var serviceEnumerator = ServiceStopHelper.Occurrences(_ds.GetServicesAtStop(_stop.Id), at).GetEnumerator();

            // board an upcoming service
            yield return new Edge(at, 0f, new BoardNode(serviceEnumerator, _ds));

            // we are also at the cluster
            yield return new Edge(at, 0f, new ClusterNode(_stop.Cluster, _ds));
        }

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
