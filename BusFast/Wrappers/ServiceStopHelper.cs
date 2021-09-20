using BusFast.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Wrappers
{
    public class ServiceStopHelper
    {
        private ServiceStop _serviceStop;

        public ServiceStopHelper(ServiceStop serviceStop)
        {
            _serviceStop = serviceStop;
        }

        public class Occurrence
        {
            public readonly ServiceStop ServiceStop;
            public readonly DateTime At;

            public Occurrence(ServiceStop serviceStop, DateTime at)
            {
                ServiceStop = serviceStop;
                At = at;
            }
        }

        public IEnumerable<Occurrence> Occurrences(DateTime after)
        {
            if (_serviceStop.Time >= after.TimeOfDay) // missed today's - go back a day
                after = after.AddDays(-1);

            after -= after.TimeOfDay;
            after += _serviceStop.Time;

            while (true) {
                after = after.AddDays(DaysAdvance(after, _serviceStop.Service.Days));
                yield return new Occurrence(_serviceStop, after);
            }
        }

        private int DaysAdvance(DateTime dt, Days days)
        {
            // the number of days from dt until the next day within days
            switch (days)
            {
                case Days.Friday:
                    switch (dt.DayOfWeek)
                    {
                        case DayOfWeek.Friday:
                            return 3;
                        case DayOfWeek.Saturday:
                            return 2;
                        default:
                            return 1;
                    }

                case Days.Saturday:
                case Days.Sunday:

                    return ((int)days + 6 - (int)dt.DayOfWeek) % 7 + 1;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
