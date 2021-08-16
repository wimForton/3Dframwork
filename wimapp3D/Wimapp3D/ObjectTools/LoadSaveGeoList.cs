using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Microsoft.Win32;

namespace GameEngine
{
    class LoadSaveGeoList
    {
        public static void Save(List<IRenderableGeo> GeoList)
        {
            string FilePath = "";
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "json";
            saveFileDialog.Filter = "(*.json, *.txt)|*.json;*.txt";
            if (saveFileDialog.ShowDialog() == true)
            {
                if (saveFileDialog.CheckPathExists)
                {
                    FilePath = saveFileDialog.FileName;
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Converters.Add(new JavaScriptDateTimeConverter());
                    serializer.NullValueHandling = NullValueHandling.Ignore;
                    serializer.Formatting = Formatting.Indented;
                    serializer.TypeNameHandling = TypeNameHandling.All;

                    using (StreamWriter sw = new StreamWriter(FilePath))
                    using (JsonWriter writer = new JsonTextWriter(sw))
                    {
                        serializer.Serialize(writer, GeoList);
                    }
                }
            }

        }
        public static void Load(List<IRenderableGeo> GeoList)
        {
            GeoList.Clear();
            Wimapp3D.MainWindow.AppWindow.MainWindowCanvas.Children.Clear();
            string FilePath = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = "json";
            openFileDialog.Filter = "(*.json, *.txt)|*.json;*.txt";
            if (openFileDialog.ShowDialog() == true)
            {
                if (openFileDialog.CheckFileExists)
                {
                    FilePath = openFileDialog.FileName;
                    var settings = new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.All,
                        ObjectCreationHandling = ObjectCreationHandling.Replace
                    };
                    IRenderableGeo.OffsetId = IRenderableGeo.HighestId + 1;
                    using (StreamReader r = new StreamReader(FilePath))
                    {
                        string json = r.ReadToEnd();
                        List<IRenderableGeo> items = JsonConvert.DeserializeObject<List<IRenderableGeo>>(json, settings);
                        GeoList.AddRange(items);
                    }
                }
                //IterateGeoTree.OffsetChildIds(GeoList);
                IterateGeoTree.RefreshNodes(GeoList);
                IterateGeoTree.ReconnectNodes(GeoList);
                IterateGeoTree.ParentChildIterate(GeoList);
            }


        }
    }
}
