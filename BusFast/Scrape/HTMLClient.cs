using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Scrape
{
    public class HTMLClient
    {
        public HTMLClient()
        {
            Directory.CreateDirectory(Location);
        }

        private string Location => Path.Combine(Path.GetTempPath(), "busfast");

        /// provides access to source data
        internal async Task<HtmlDocument> Load(string key, string url)
        {
            HtmlDocument document;

            var path = Path.Combine(Location, key + ".html");
            if (File.Exists(path))
            {
                document = new HtmlDocument();
                document.Load(path);
            }
            else
            {
                var web = new HtmlWeb();
                document = await web.LoadFromWebAsync(url);
                using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    document.Save(fs);
                    fs.Flush();
                }
            }

            return document;
        }
    }
}
