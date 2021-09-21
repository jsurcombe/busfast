using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Wrappers.Journey
{
    public class BoardEdge : Edge
    {
        private readonly ServiceStopHelper.Occurrence _serviceStop;

        public BoardEdge(DateTime at, ServiceStopNode to) : base(at, 1f, to)
        {
            _serviceStop = to.ServiceStop;
        }

        public override string Describe()
        {
            var bound = _serviceStop.ServiceStop.Stop.Bound;
            return $"Board the {(bound != null ? bound.ToLower() + " " : "")}route {_serviceStop.ServiceStop.Service.Route.Name} bus";
        }
    }
}
