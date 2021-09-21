using BusFast.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Wrappers.Journey
{
    public class ServiceStopNode : Node
    {
        private ServiceStopHelper.Occurrence _serviceStop;

        public ServiceStopNode(ServiceStopHelper.Occurrence serviceStop, DataService ds) : base(ds)
        {
            _serviceStop = serviceStop;
        }

        public override IEnumerable<Edge> Edges(DateTime at)
        {
            // stay on the bus
            var next = _serviceStop.Next;
            if (next != null) // can't stay on the bus - this is the end of the service
                yield return new Edge(next.At, 0f, new ServiceStopNode(next, _ds));

            // get off the bus
            yield return new Edge(_serviceStop.At, 0f, new StopNode(_serviceStop.ServiceStop.Stop, _ds));
        }

        public override int GetHashCode()
        {
            return _serviceStop.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is ServiceStopNode bo && bo._serviceStop.Equals(_serviceStop);
        }
    }
}
