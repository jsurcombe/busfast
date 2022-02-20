using System;

namespace BusFast.Scrape
{
    public class TripStop
    {
        public int Stop_Id { get; set; }
        public string Departure_Time { get; set; }
        public int DaysOffset { get; set; }
        public TimeSpan Time => TimeSpan.Parse(Departure_Time) + new TimeSpan(DaysOffset, 0, 0, 0);
    }
}
