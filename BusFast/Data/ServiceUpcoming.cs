using BusFast.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Data
{
    public class ServiceUpcoming
    {
        public ServiceUpcoming(ServiceStopHelper.Occurrence occurrence)
        {
            Service = new ServiceItem(occurrence.ServiceStop.Service);
            At = occurrence.At;
            StopBound = occurrence.ServiceStop.Stop.Bound;
        }

        public ServiceItem Service { get; }
        public DateTime At { get; }
        public string StopBound { get; }
    }
}
