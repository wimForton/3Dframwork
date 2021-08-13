using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GameEngine
{
    class LoadSaveGeoList
    {
        public static void Save(List<IRenderableGeo> GeoList)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;
            serializer.Formatting = Formatting.Indented;
            serializer.TypeNameHandling = TypeNameHandling.All;

            using (StreamWriter sw = new StreamWriter(@"H:\cursus_informatica\3Dapplication\json.txt"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, GeoList);
            }
        }
        public static void Load(List<IRenderableGeo> GeoList)
        {
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All,
                ObjectCreationHandling = ObjectCreationHandling.Replace
            };
            using (StreamReader r = new StreamReader(@"H:\cursus_informatica\3Dapplication\json.txt"))
            {
                string json = r.ReadToEnd();
                List<IRenderableGeo> items = JsonConvert.DeserializeObject<List<IRenderableGeo>>(json, settings);
                GeoList.AddRange(items);
            }
            
        }
    }
}
