using Newtonsoft.Json;
namespace Models
{
    public class RadarList
    {
        [JsonProperty("radar")]
        public List<Radar> Radar { get; set; }

    }
}
