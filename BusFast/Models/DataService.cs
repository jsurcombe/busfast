using BusFast.Foundation;
using BusFast.Scrape;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Models
{
    // provides all the data
    public class DataService
    {
        private readonly Task _loadTask;
        private Stop[] _stops;
        private Dictionary<long, Stop> _stopDictionary;
        private ILookup<string, ServiceStop> _servicesByCluster;
        private ILookup<long, ServiceStop> _servicesByStop;

        internal IEnumerable<ServiceStop> GetServicesAtStop(long id)
        {
            _loadTask.Wait();
            return _servicesByStop[id];
        }

        private Dictionary<string, Service> _serviceDictionary;
        private Cluster[] _clusters;
        private Dictionary<string, Cluster> _clusterDictionary;
        private Dictionary<long, Dictionary<Stop, TimeSpan>> _stopPairs;

        public DataService(DataLoader dataLoader)
        {
            _loadTask = dataLoader.LoadData().ContinueWith(r =>
            {
                var routes = Convert(r.Result);
                var allServices = routes.SelectMany(rn => rn.Services);

                _stops = allServices.SelectMany(svc => svc.Stops).Select(s => s.Stop).GroupBy(s => s.Id).Select(g => g.First()).ToArray();

                foreach (var s in _stops)
                    FixStopName(s);

                _stopDictionary = _stops.ToDictionary(s => s.Id);

                foreach (var s in allServices)
                    FixStops(s);

                // canonicalise stops
                foreach (var s in allServices.SelectMany(srv => srv.Stops))
                    s.Stop = _stopDictionary[s.Stop.Id];

                _clusters = _stops.GroupBy(s => ClusterId(ClusterName(s.Name)))
                    .Select(g =>
                    {
                        var name = ClusterName(g.First().Name);
                        return new Cluster()
                        {
                            Name = name,
                            Id = g.Key,
                            Tokens = name.SearchTokens(),
                            Stops = g.ToArray()
                        };
                    })
                    .OrderBy(c => c.Id.Replace("-", ""))
                    .ToArray();

                _clusterDictionary = _clusters.ToDictionary(c => c.Id);

                foreach (var c in _clusters)
                    foreach (var s in c.Stops)
                        s.Cluster = c;

                _servicesByCluster = allServices.SelectMany(svc => svc.Stops).ToLookup(ss => ss.Stop.Cluster.Id);
                _servicesByStop = allServices.SelectMany(svc => svc.Stops).ToLookup(ss => ss.Stop.Id);
                _serviceDictionary = allServices.ToDictionary(svc => svc.Id);

                foreach (var route in routes)
                {
                    // find service returns
                    foreach (var svc in route.Services.Where(si => si.Direction == Direction.Outbound))
                    {
                        var terminal = svc.Stops[svc.Stops.Count - 1];

                        svc.Return = route.Services.Where(si => si.Direction == Direction.Inbound
                            && si.DayOfWeek == svc.DayOfWeek
                            && si.Stops[0].Stop.Cluster == terminal.Stop.Cluster
                            && si.Stops[0].Time >= terminal.Time)
                            .FirstOrDefault();
                    }
                }

                var stopPairs = new Dictionary<long, Dictionary<Stop, List<TimeSpan>>>();

                // build walking routes
                foreach (var s in allServices)
                {
                    for (int i = 1; i < s.Stops.Count; i++)
                    {
                        var from = s.Stops[i - 1];
                        if (!stopPairs.TryGetValue(from.Stop.Id, out var toDict))
                            stopPairs[from.Stop.Id] = toDict = new Dictionary<Stop, List<TimeSpan>>();

                        var to = s.Stops[i];
                        if (!toDict.TryGetValue(to.Stop, out var l))
                            toDict[to.Stop] = l = new List<TimeSpan>();

                        if (to.Time < from.Time)
                            throw new NotImplementedException();

                        l.Add(to.Time - from.Time);
                    }
                }

                _stopPairs = stopPairs.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToDictionary(s => s.Key, s =>
                {
                    return TimeSpan.FromMinutes(s.Value.Select(t => t.TotalMinutes).Sum() / s.Value.Count);
                }));

            });
        }

        private Route[] Convert(Scrape.Data result)
        {
            var filteredTimetables = result.Timetables.Where(ti => !ti.Key.StartsWith("Z")); // remove school buses            

            // pre-processing
            foreach (var t in filteredTimetables)
                foreach (var s in t.Value.Timetable)
                    foreach (var tr in s.Value.Days)
                        foreach (var tta in tr.Trips)
                        {
                            // if this trip goes after 6pm, then any pre-6am stop is the next day
                            if (tta.Stops.Any(s => s.Departure_Time.CompareTo("18:00") > 0)) {
                                foreach (var ss in tta.Stops.Where(s => s.Departure_Time.CompareTo("06:00") < 0))
                                    ss.DaysOffset = 1;
                            }
                        }

            return filteredTimetables.Select(kvp => new Route(kvp.Key, kvp.Value)).ToArray();
        }

        private void FixStops(Service s)
        {
            var VALE_KIOSK_NORTHBOUND = 890000691L;
            var TRINITY_SQUARE_SOUTHBOUND = 890000603L;
            var VALE_CHURCH_SOUTHBOUND = 890000499L;

            // fix bug in route 13
            for (var i = 1; i < s.Stops.Count; i++)
            {
                if (s.Stops[i - 1].Stop.Id == VALE_KIOSK_NORTHBOUND && s.Stops[i].Stop.Id == TRINITY_SQUARE_SOUTHBOUND)
                    s.Stops[i].Stop = _stopDictionary[VALE_CHURCH_SOUTHBOUND];
            }
        }

        public Dictionary<Stop, TimeSpan> GetNeighbours(long stopId)
        {
            if (_stopPairs.TryGetValue(stopId, out var result))
                return result;
            else
                return new();
        }

        private void FixStopName(Stop s)
        {
            s.Name = string.Join(' ', s.Name.Split(' ', StringSplitOptions.RemoveEmptyEntries));

            s.Name = s.Name switch
            {
                "La Coutures - Northbound" => "La Couture - Northbound",
                _ => s.Name
            };
        }

        private string ClusterId(string clusterName)
        {
            return new string(clusterName.ToLower().Where(c => char.IsLetter(c) || c == ' ').ToArray()).Replace(' ', '-');
        }

        private string ClusterName(string stopName)
        {
            var i = stopName.IndexOf('-');
            if (i == -1)
                return stopName;
            else
                return stopName.Substring(0, i).Trim();
        }

        internal Service GetService(string id) { _loadTask.Wait(); return _serviceDictionary[id]; }

        internal Cluster GetCluster(string id) { _loadTask.Wait(); return _clusterDictionary[id]; }

        internal Stop GetStop(long id) { _loadTask.Wait(); return _stopDictionary[id]; }

        public Stop[] Stops
        {
            get
            {
                _loadTask.Wait(); // block if we haven't loaded yet
                return _stops;
            }
        }

        public Cluster[] Clusters
        {
            get
            {
                _loadTask.Wait();
                return _clusters;
            }
        }

        public IEnumerable<ServiceStop> GetServicesAt(string clusterId)
        {
            _loadTask.Wait();
            return _servicesByCluster[clusterId];
        }
    }
}
