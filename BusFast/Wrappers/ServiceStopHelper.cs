using BusFast.Models;
using Priority_Queue;
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

            public Occurrence? Next
            {
                get
                {
                    if (ServiceStop.Next == null)
                        return null;
                    else
                        return new Occurrence(ServiceStop.Next, At - ServiceStop.Time + ServiceStop.Next.Time);
                }
            }

            public override int GetHashCode()
            {
                return (ServiceStop, At).GetHashCode();
            }

            public override bool Equals(object obj)
            {
                return obj is Occurrence oo && oo.ServiceStop == ServiceStop && oo.At == At;
            }
        }

        public IEnumerable<Occurrence> Occurrences(DateTime after)
        {
            if (_serviceStop.Time >= after.TimeOfDay) // we will catch the first one
                after = after.AddDays(-1);

            after -= after.TimeOfDay;
            after += _serviceStop.Time;

            while (true)
            {
                after = after.AddDays(DaysAdvance(after, _serviceStop.Service.DayOfWeek));
                yield return new Occurrence(_serviceStop, after);
            }
        }

        private int DaysAdvance(DateTime dt, DayOfWeek day)
        {
            return ((int)day + 6 - (int)dt.DayOfWeek) % 7 + 1;
        }

        public static IEnumerable<Occurrence> Occurrences(IEnumerable<ServiceStop> services, DateTime at)
        {
            // board the next bus from this stop
            var iterators = new SimplePriorityQueue<IEnumerator<Occurrence>>();

            foreach (var s in services)
            {
                var sh = new ServiceStopHelper(s);
                var occ = sh.Occurrences(at).GetEnumerator();
                occ.MoveNext();

                iterators.Enqueue(occ, (float)(occ.Current.At - DateTime.Now).TotalMinutes);
            }

            while (true)
            {
                var n = iterators.Dequeue();
                yield return n.Current;

                n.MoveNext();
                iterators.Enqueue(n, (float)(n.Current.At - DateTime.Now).TotalMinutes);
            }
        }

    }
}
