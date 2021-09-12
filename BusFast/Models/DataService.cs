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

        public DataService(DataLoader dataLoader)
        {
            _loadTask = dataLoader.LoadRoutes().ContinueWith(r =>
            {
                _routes = r.Result.ToDictionary(ri => ri.Name);
                _stops = _routes.Values.SelectMany(rn => rn.Services).SelectMany(svc => svc.Stops).Select(s => s.Stop).GroupBy(s => s.Id).Select(g => g.First()).ToArray();
            });
        }

        public Stop[] Stops
        {
            get
            {
                _loadTask.Wait(); // block if we haven't loaded yet
                return _stops;
            }
        }
    }
}
