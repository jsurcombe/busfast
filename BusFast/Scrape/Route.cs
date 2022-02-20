using BusFast.Models;
using System.Collections.Generic;

namespace BusFast.Scrape
{
    public class Route
    {
        public string Service_Name { get; set; }
        public Dictionary<Direction, DirectedRoute> Timetable { get; set; }
    }
}
