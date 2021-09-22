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
            yield return new WaitEdge(at, 2f, new BoardNode(serviceEnumerator, _ds));

            // we are also at the cluster - which allows us to transition to another stop in the cluster
            yield return new WaitEdge(at, 1f, new ClusterNode(Stop.Cluster, _ds));

            // walk to a neighbouring stop
            foreach (var s in _ds.GetNeighbours(Stop.Id))
            {
                var timeEstimate = (s.Value * 4) + TimeSpan.FromMinutes(2);
                if (timeEstimate.TotalMinutes < 0)
                    throw new NotImplementedException();
                    yield return new WalkEdge(at + timeEstimate, timeEstimate, new StopNode(s.Key, _ds));
            }
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
