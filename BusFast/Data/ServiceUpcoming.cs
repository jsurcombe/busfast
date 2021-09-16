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
            Stop = new ClusterStopItem(occurrence.ServiceStop.Stop);
        }

        public ServiceItem Service { get; }
        public DateTime At { get; }
        public ClusterStopItem Stop { get; }
    }
}
