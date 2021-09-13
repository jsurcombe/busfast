using BusFast.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Data
{
    public class StopItem
    {
        public StopItem(Stop stop)
        {
            Id = stop.Id;
            Name = stop.Name;
        }

        public int Id { get; }
        public string Name { get; }
    }
}
