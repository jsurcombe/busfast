using BusFast.Data;
using BusFast.Foundation;
using BusFast.Models;
using BusFast.Wrappers;
using BusFast.Wrappers.Journey;
using Microsoft.AspNetCore.Mvc;
using Priority_Queue;
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
            var unvisited = new SimplePriorityQueue<Cursor>();

            var thisPath = new Cursor(Globals.GuernseyNow, 0f, new ClusterNode(_ds.GetCluster(fromCluster)));

            unvisited.Enqueue(thisPath, thisPath.Priority);

            var visited = new HashSet<Node>();

            while (true)
            {
                // take the first path
                Cursor c;
                do
                {
                    if (unvisited.Count == 0) // nothing left
                        yield break;

                    c = unvisited.Poll();
                } while (visited.Contains(c.To));

                yield return c;

                // iterate the edges from this location
                if (_edges.TryGetValue(c.To, out var edges))
                {
                    foreach (var e in edges)
                    {
                        if (visited.Contains(e.To))
                            continue; // already visited

                        if (!filter(e)) // excluded edge
                            continue;

                        unvisited.Add(new Cursor(c, e));
                    }
                }

                visited.Add(c.To);
            }
        }
    }
}
