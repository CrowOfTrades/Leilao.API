using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Leilao.Infrastructure.Storage.Storage.Services
{
    class ReadJsonService
    {
        public static Item LoadJson()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "appsettings.json");
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<Item>(json);
            }
        }

        public class Item
        {
            public string connString;
        }
    }
}
