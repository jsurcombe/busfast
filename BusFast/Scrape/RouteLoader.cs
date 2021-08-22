using BusFast.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Scrape
{
    public class RouteLoader
    {
        public RouteLoader(string route)
        {
            _route = route;
        }

        private readonly string _route;

        public async Task<Route> LoadRoute()
        {
            HtmlDocument document;
            if (File.Exists(_route))
            {
                document = new HtmlDocument();
                document.Load(_route);
            }
            else
            {
                var web = new HtmlWeb();
                document = await web.LoadFromWebAsync($"http://buses.gg/routes_and_times/timetables/{_route}/FALSE");
                using (var fs = new FileStream(_route, FileMode.Create, FileAccess.Write))
                {
                    document.Save(fs);
                    fs.Flush();
                }
            }

            var res = new Route();
            res.Name = _route;
            res.Services = document.DocumentNode.SelectNodes("//div[@class='t-table']").SelectMany(n => LoadServices(n)).ToArray();

            return res;
        }

        private IEnumerable<Service> LoadServices(HtmlNode n)
        {
            throw new NotImplementedException();
        }
    }
}
