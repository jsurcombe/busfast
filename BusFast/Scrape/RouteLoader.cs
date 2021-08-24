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

        private Service[] LoadServices(HtmlNode n)
        {
            var stops = n.SelectNodes("table[@class='headers']/tbody/tr/comment()[1]").Select(n => n.InnerHtml).Select(s => int.Parse(new string(s.Where(c => char.IsDigit(c)).ToArray()))).ToArray();

            var div = n.SelectSingleNode("div[1]");

            var divId = div.Id;
            var dayIdCode = divId.Split('-').Last();

            var daysCode = Enum.Parse<DaysCode>(dayIdCode, true);

            var stopServiceSets = div.SelectNodes("table/tbody/tr").ToArray();

            var serviceCount = stopServiceSets[0].SelectNodes("td").Where(n => !n.HasClass("StopHeader") && !n.HasClass("end-row")).Count();

            var services = Enumerable.Range(0, serviceCount).Select(i => new Service() { Days = daysCode, Stops = new List<ServiceStop>() }).ToArray();

            for (int i = 0; i < stops.Length; i++)
            {
                var srcServices = stopServiceSets[i].SelectNodes("td").Where(n => !n.HasClass("StopHeader") && !n.HasClass("end-row")).ToArray();
                for (int j = 0; j < serviceCount; j++)
                {
                    var t = srcServices[j].InnerText;
                    if (t != " - ")
                    {
                        services[j].Stops.Add(new ServiceStop() { StopId = stops[i], Time = TimeSpan.Parse(t) });
                    }
                }
            }

            return services;
        }
    }
}
