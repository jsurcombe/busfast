using BusFast.Scrape;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BusFast.Controllers
{
    [ApiController]
    [Route("scrape")]
    public class ScrapeController
    {
        [Route("timetables")]
        [HttpPut]
        public async Task UpdateRoutes()
        {
            await UpdateRoute("11");
        }

        private async Task UpdateRoute(string name)
        {
            var t = new RouteLoader(name);

            var r = await t.LoadRoute();
        }
    }
}
