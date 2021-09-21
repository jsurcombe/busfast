using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Wrappers.Journey
{
    public class BoardEdge : Edge
    {
        private readonly ServiceStopHelper.Occurrence _serviceStop;

        public BoardEdge(DateTime at, float cost, ServiceStopNode to) : base(at, cost, to)
        {
            _serviceStop = to.ServiceStop;
        }

        public override string Describe()
        {
            return $"Board the {_serviceStop.ServiceStop.Stop.Bound.ToLower()} route {_serviceStop.ServiceStop.Service.Route.Name} bus";
        }
    }
}
