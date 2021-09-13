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
            Stop = new StopItem(serviceStop.Stop);
            Time = serviceStop.Time;
        }

        public StopItem Stop { get; }
        [JsonConverter(typeof(TimeConverter))]
        public TimeSpan Time { get; }
    }
}
