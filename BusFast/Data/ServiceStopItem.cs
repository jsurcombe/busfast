using BusFast.Models;
using System;
using System.Text.Json.Serialization;

namespace BusFast.Data
{
    public class ServiceStopItem
    {
        public ServiceStopItem(ServiceStop serviceStop)
        {
            StopId = serviceStop.Stop.Id;
            Cluster = new ClusterItem(serviceStop.Stop.Cluster);
            Time = serviceStop.Time;
        }

        public int StopId { get; }
        public ClusterItem Cluster { get; }
        [JsonConverter(typeof(TimeConverter))]
        public TimeSpan Time { get; }
    }
}
