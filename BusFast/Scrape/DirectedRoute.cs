using System.Collections.Generic;

namespace BusFast.Scrape
{
    public class DirectedRoute
    {
        public Service? Mon_Fri { get; set; }
        public Service? Sat { get; set; }
        public Service? Sun { get; set; }
        public Stop[] Stops_List { get; set; }
        public IEnumerable<Service> Days
        {
            get
            {
                if (Mon_Fri != null) yield return Mon_Fri;
                if (Sat != null) yield return Sat;
                if (Sun != null) yield return Sun;
            }
        }
    }
}
