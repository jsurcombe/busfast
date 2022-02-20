using BusFast.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusFast.Scrape
{
    public class DataLoader
    {
        private readonly HttpClient _client;

        public DataLoader(HttpClient client)
        {
            _client = client;
        }

        public async Task<Data> LoadData()
        {
            var dataJSON = await _client.GetStringAsync("https://gsybus-admin.libertybus.je/cache/timetables/timetable_full.json");
            return JsonSerializer.Deserialize<Data>(dataJSON, new JsonSerializerOptions(JsonSerializerDefaults.Web));
        }

    }
}
