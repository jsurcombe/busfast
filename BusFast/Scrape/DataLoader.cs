using BusFast.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Scrape
{
    public class DataLoader
    {
        public DataLoader(HTMLClient httpClient)
        {
            _htmlClient = httpClient;
        }

        private readonly HTMLClient _htmlClient;

        public async Task<Route[]> LoadRoutes()
        {
            HtmlDocument document = await _htmlClient.Load($"timetables", $"http://buses.gg/routes_and_times/timetables");

            var routes = document.DocumentNode.SelectNodes("//ul[@id='main-timetable-list']/li").Select(n => LoadRoute(n)).ToArray();

            foreach (var r in routes)
                await LoadRouteServices(r);

            return routes;
        }

        private Route LoadRoute(HtmlNode n)
        {
            var route = new Route();
            route.Name = n.SelectSingleNode(".//div[@class='tt-key']").InnerText;
            route.Description = n.SelectSingleNode(".//div[@class='tt-text']").InnerText;
            return route;
        }

        private async Task LoadRouteServices(Route route)
        {
            HtmlDocument document = await _htmlClient.Load($"route{route.Name}", $"http://buses.gg/routes_and_times/timetables/{route.Name}/FALSE");

            route.Services = document.DocumentNode.SelectNodes("//div[@class='t-table']").SelectMany(n => LoadServices(route, n)).ToArray();
        }

        private Service[] LoadServices(Route route, HtmlNode n)
        {
            var stops = n.SelectNodes("table[@class='headers']/tbody/tr").Select(n => LoadStop(n)).ToArray();

            var div = n.SelectSingleNode("div[1]");

            var divId = div.Id;
            var dayIdCode = divId.Split('-').Last();

            var daysCode = Enum.Parse<DaysCode>(dayIdCode, true);

            var stopServiceSets = div.SelectNodes("table/tbody/tr").ToArray();

            var serviceCount = stopServiceSets[0].SelectNodes("td").Where(n => !n.HasClass("StopHeader") && !n.HasClass("end-row")).Count();

            var services = Enumerable.Range(0, serviceCount).Select(i => new Service(route) { Days = daysCode, Stops = new List<ServiceStop>() }).ToArray();

            var firstCells = stopServiceSets[0].SelectNodes("td").Where(n => !n.HasClass("StopHeader") && !n.HasClass("end-row")).ToArray();

            for (int j = 0; j < serviceCount; j++)
            {
                var t = firstCells[j].Attributes["data"].Value;
                services[j].Id = t.Substring(0, t.IndexOf('@'));
            }

            for (int i = 0; i < stops.Length; i++)
            {
                var srcServices = stopServiceSets[i].SelectNodes("td").Where(n => !n.HasClass("StopHeader") && !n.HasClass("end-row")).ToArray();
                for (int j = 0; j < serviceCount; j++)
                {
                    var t = srcServices[j].InnerText;
                    if (t != " - ")
                    {
                        var service = services[j];
                        service.Stops.Add(new ServiceStop(service) { Stop = stops[i], Time = TimeSpan.Parse(t) });
                    }
                }
            }

            return services;
        }

        private Stop LoadStop(HtmlNode n)
        {
            var res = new Stop();
            var s = n.SelectSingleNode("comment()[1]").InnerHtml;
            res.Id = int.Parse(new string(s.Where(c => char.IsDigit(c)).ToArray()));
            res.Name = n.SelectSingleNode("th/span").InnerText;
            return res;
        }
    }
}
