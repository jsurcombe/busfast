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
        private ILookup<int, ServiceStop> _servicesByStop;
        private Dictionary<string, Service> _serviceDictionary;

        public DataService(DataLoader dataLoader)
        {
            _loadTask = dataLoader.LoadRoutes().ContinueWith(r =>
            {
                _routes = r.Result.ToDictionary(ri => ri.Name);
                _stops = r.Result.SelectMany(rn => rn.Services).SelectMany(svc => svc.Stops).Select(s => s.Stop).GroupBy(s => s.Id).Select(g => g.First()).ToArray();
                _stopDictionary = _stops.ToDictionary(s => s.Id);
                _servicesByStop = r.Result.SelectMany(rn => rn.Services).SelectMany(svc => svc.Stops).ToLookup(ss => ss.Stop.Id);
                _serviceDictionary = r.Result.SelectMany(rn => rn.Services).ToDictionary(svc => svc.Id);
            });
        }

        internal Service GetService(string id) { _loadTask.Wait(); return _serviceDictionary[id]; }

        internal Stop GetStop(int id) { _loadTask.Wait(); return _stopDictionary[id]; }

        public Stop[] Stops
        {
            get
            {
                _loadTask.Wait(); // block if we haven't loaded yet
                return _stops;
            }
        }

        public IEnumerable<ServiceStop> GetServicesAt(int stopId)
        {
            _loadTask.Wait();
            return _servicesByStop[stopId];
        }
    }
}
