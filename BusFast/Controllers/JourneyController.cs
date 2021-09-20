using BusFast.Data;
using BusFast.Foundation;
using BusFast.Models;
using BusFast.Wrappers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Controllers
{
    [ApiController]
    [Route("api/journey")]
    public class JourneyController
    {
        private readonly DataService _ds;

        public JourneyController(DataService ds)
        {
            _ds = ds;
        }

        [HttpGet]
        public Journey Get(string fromCluster, string toCluster)
        {
            var fromState = new AtClusterState(Globals.GuernseyNow, _ds.GetCluster(fromCluster));

            // dijkstra
            var visited = new HashSet<JourneyState>();

            visited.Add(fromState);

            foreach (var e in fromState.NextStates)
            {

            }
        }
    }
}
