using BusFast.Data;
using BusFast.Foundation;
using BusFast.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Controllers
{
    [ApiController]
    [Route("api/clusters")]
    public class ClusterController
    {
        private readonly DataService _ds;

        public ClusterController(DataService ds)
        {
            _ds = ds;
        }

        [HttpGet]
        public ClusterItem[] Search(string q)
        {
            if (string.IsNullOrEmpty(q))
                return new ClusterItem[] { };
            else
            {
                var tokens = q.SearchTokens();
                if (tokens.Length == 0)
                    return new ClusterItem[] { };
                else
                    return _ds.Clusters.Where(c => tokens.All(t => c.Tokens.Any(ct => ct.StartsWith(t)))).Take(10).Select(c => new ClusterItem(c)).ToArray();
            }
        }


        [HttpGet]
        [Route("{id}")]
        public ClusterItem View(string id) => new ClusterItem(_ds.GetCluster(id));
    }
}
