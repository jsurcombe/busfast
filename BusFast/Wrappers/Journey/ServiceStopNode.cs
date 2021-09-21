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

            if (next == null) // this service terminates here
            {
                var returnService = ServiceStop.ServiceStop.Service.Return;
                if (returnService != null) // if we can ride the bus back
                {
                    var returnFromStop = returnService.Stops[0];
                    next = new ServiceStopHelper.Occurrence(returnFromStop, at - ServiceStop.ServiceStop.Time + returnFromStop.Time);
                }
            }

            if (next != null) // we can only remain on the bus if it's not terminating
                yield return new WaitEdge(next.At, 0f, new ServiceStopNode(next, _ds)); // remain on the bus

            // we can always get off
            yield return new AlightEdge(ServiceStop.At, new StopNode(ServiceStop.ServiceStop.Stop, _ds));
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
