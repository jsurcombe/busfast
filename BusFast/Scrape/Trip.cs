using System.Collections.Generic;
using System.Linq;

namespace BusFast.Scrape
{
    public class Trip
    {
        public int Trip_Id { get; set; }
        public TripStop[] Stops { get; set; }
        public Dictionary<Day, bool> Days_Of_Operation { get; set; }
    }
}
