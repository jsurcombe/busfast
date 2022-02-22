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

        private string Location => Path.Combine(Path.GetTempPath(), "busfast");

        public async Task<Data> LoadData()
        {
            Directory.CreateDirectory(Location);
            var cacheLocation = Path.Combine(Location, "timetable_full.json");
            string dataJSON;

            if (File.Exists(cacheLocation) && (DateTime.UtcNow - File.GetLastWriteTimeUtc(cacheLocation)).TotalDays < 1.0)
                dataJSON = File.ReadAllText(cacheLocation);
            else
            {
                try
                {
                    dataJSON = await _client.GetStringAsync("https://gsybus-admin.libertybus.je/cache/timetables/timetable_full.json");
                    File.WriteAllText(cacheLocation, dataJSON);
                }
                catch (Exception)
                {
                    // fall back to last copy
                    dataJSON = File.ReadAllText(cacheLocation);
                }
            }
            return JsonSerializer.Deserialize<Data>(dataJSON, new JsonSerializerOptions(JsonSerializerDefaults.Web));
        }

    }
}
