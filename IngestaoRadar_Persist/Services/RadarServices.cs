using Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Services
{
    public class RadarServices
    {
        public void InsertFileOnSQL(string jsonString)
        {
            var json = JsonConvert.DeserializeObject<RadarList>(jsonString);
            foreach (var radar in json.Radar)
            {
                Console.WriteLine(radar);
            }
        }
    }
}
