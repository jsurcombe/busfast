using BusFast.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusFast.Data
{
    public class ServiceStopItem
    {
        public ServiceStopItem(ServiceStop serviceStop)
        {
            StopCluster = new ClusterItem(serviceStop.Stop.Cluster);
            Time = serviceStop.Time;
        }

        public ClusterItem StopCluster { get; }
        [JsonConverter(typeof(TimeConverter))]
        public TimeSpan Time { get; }
    }
}
