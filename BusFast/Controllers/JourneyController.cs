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
            var start = new Cursor(Globals.GuernseyNow, 0f, new ClusterNode(_ds.GetCluster(fromCluster), _ds));

            var toNode = new ClusterNode(_ds.GetCluster(toCluster), _ds);

            var sr = ShortestRoutes(start).First(cn => cn.Node.Equals(toNode));

            throw new NotImplementedException();
        }

        private IEnumerable<Cursor> ShortestRoutes(Cursor start)
        {

            var unvisited = new SimplePriorityQueue<Cursor>();

            unvisited.Enqueue(start, start.Priority);

            var visited = new HashSet<Node>();

            while (true)
            {
                // take the first path
                Cursor c;
                do
                {
                    if (!unvisited.TryDequeue(out c))
                        yield break;
                } while (visited.Contains(c.Node));

                yield return c;

                foreach (var e in c.Edges)
                {
                    if (visited.Contains(e.To))
                        continue; // already visited

                    var nextCursor = new Cursor(c, e);

                    unvisited.Enqueue(nextCursor, nextCursor.Priority);
                }

                visited.Add(c.Node);
            }
        }
    }
}
