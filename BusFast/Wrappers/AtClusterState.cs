using BusFast.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Wrappers
{
    // represents standing at a cluster
    public class AtClusterState : JourneyState
    {
        private Cluster _cluster;

        public AtClusterState(DateTime at, Cluster cluster) : base(at)
        {
            _cluster = cluster;
        }

        public IEnumerable<JourneyState> NextStates
        {
            get
            {
                // be standing at any stop in this cluster
                foreach (var s in _cluster.Stops)
                    yield return new AtStopState(s);
            }
        }
    }
}
