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
        public readonly Stop Stop;

        public StopNode(Stop stop, DataService ds) : base(ds) { Stop = stop; }

        public override IEnumerable<Edge> Edges(DateTime at)
        {
            // get an iterator for services leaving here after now
            var serviceEnumerator = ServiceStopHelper.Occurrences(_ds.GetServicesAtStop(Stop.Id), at).GetEnumerator();

            // decide to board an upcoming service
            yield return new WaitEdge(at, 0f, new BoardNode(serviceEnumerator, _ds));

            // we are also at the cluster
            yield return new WaitEdge(at, 0f, new ClusterNode(Stop.Cluster, _ds));
        }

        public override int GetHashCode()
        {
            return Stop.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is StopNode ass && ass.Stop == Stop;
        }
    }
}
