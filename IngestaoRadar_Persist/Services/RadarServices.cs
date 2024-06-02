using Models;
using Newtonsoft.Json;
using Repositories;

namespace Services
{
    public class RadarServices
    {
        public void InsertFileOnSQL(string jsonString)
        {
            var json = JsonConvert.DeserializeObject<RadarList>(jsonString);
            foreach (var radar in json.Radar)
            {
                new RadarRepository().Insert(radar);
            }
        }
    }
}
