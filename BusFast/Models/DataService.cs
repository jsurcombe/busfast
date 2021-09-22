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
        private Dictionary<int, Stop> _stopDictionary;
        private ILookup<string, ServiceStop> _servicesByCluster;
        private ILookup<int, ServiceStop> _servicesByStop;

        internal IEnumerable<ServiceStop> GetServicesAtStop(int id)
        {
            _loadTask.Wait();
            return _servicesByStop[id];
        }

        private Dictionary<string, Service> _serviceDictionary;
        private Cluster[] _clusters;
        private Dictionary<string, Cluster> _clusterDictionary;
        private Dictionary<int, Dictionary<Stop, TimeSpan>> _stopPairs;

        public DataService(DataLoader dataLoader)
        {
            _loadTask = dataLoader.LoadRoutes().ContinueWith(r =>
            {
                var allServices = r.Result.SelectMany(rn => rn.Services);

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

                foreach (var route in r.Result)
                {
                    // find service returns
                    foreach (var svc in route.Services.Where(si => si.Direction == Direction.Outbound))
                    {
                        var terminal = svc.Stops[svc.Stops.Count - 1];

                        svc.Return = route.Services.Where(si => si.Direction == Direction.Inbound
                            && si.Days == svc.Days
                            && si.Stops[0].Stop.Cluster == terminal.Stop.Cluster
                            && si.Stops[0].Time >= terminal.Time)
                            .FirstOrDefault();
                    }
                }

                var stopPairs = new Dictionary<int, Dictionary<Stop, List<TimeSpan>>>();

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

        private void FixStops(Service s)
        {
            var VALE_KIOSK_NORTHBOUND = 890000691;
            var TRINITY_SQUARE_SOUTHBOUND = 890000603;
            var VALE_CHURCH_SOUTHBOUND = 890000499;

            // fix bug in route 13
            for (var i = 1; i < s.Stops.Count; i++)
            {
                if (s.Stops[i - 1].Stop.Id == VALE_KIOSK_NORTHBOUND && s.Stops[i].Stop.Id == TRINITY_SQUARE_SOUTHBOUND)
                    s.Stops[i].Stop = _stopDictionary[VALE_CHURCH_SOUTHBOUND];
            }
        }

        public Dictionary<Stop, TimeSpan> GetNeighbours(int stopId)
        {
            return _stopPairs[stopId];
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

        internal Stop GetStop(int id) { _loadTask.Wait(); return _stopDictionary[id]; }

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
