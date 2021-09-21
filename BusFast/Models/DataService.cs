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
        private Dictionary<string, Route> _routes;
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

        public DataService(DataLoader dataLoader)
        {
            _loadTask = dataLoader.LoadRoutes().ContinueWith(r =>
            {
                _routes = r.Result.ToDictionary(ri => ri.Name);
                _stops = r.Result.SelectMany(rn => rn.Services).SelectMany(svc => svc.Stops).Select(s => s.Stop).GroupBy(s => s.Id).Select(g => g.First()).ToArray();

                foreach (var s in _stops)
                    FixStopName(s);

                _stopDictionary = _stops.ToDictionary(s => s.Id);

                // canonicalise stops
                foreach (var s in r.Result.SelectMany(route => route.Services).SelectMany(srv => srv.Stops))
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

                _servicesByCluster = r.Result.SelectMany(rn => rn.Services).SelectMany(svc => svc.Stops).ToLookup(ss => ss.Stop.Cluster.Id);
                _servicesByStop = r.Result.SelectMany(rn => rn.Services).SelectMany(svc => svc.Stops).ToLookup(ss => ss.Stop.Id);
                _serviceDictionary = r.Result.SelectMany(rn => rn.Services).ToDictionary(svc => svc.Id);

            });
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
