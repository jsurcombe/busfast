using BusFast.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Wrappers.Journey
{
    public class ServiceStopNode : Node
    {
        public readonly ServiceStopHelper.Occurrence ServiceStop;

        public ServiceStopNode(ServiceStopHelper.Occurrence serviceStop, DataService ds) : base(ds)
        {
            ServiceStop = serviceStop;
        }

        public override IEnumerable<Edge> Edges(DateTime at)
        {
            // stay on the bus
            var next = ServiceStop.Next;
            if (next != null) // can't stay on the bus - this is the end of the service
                yield return new WaitEdge(next.At, 0f, new ServiceStopNode(next, _ds)); // remain on the bus

            // get off the bus
            yield return new AlightEdge(ServiceStop.At, 0f, new StopNode(ServiceStop.ServiceStop.Stop, _ds));
        }

        public override int GetHashCode()
        {
            return ServiceStop.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is ServiceStopNode bo && bo.ServiceStop.Equals(ServiceStop);
        }
    }
}
