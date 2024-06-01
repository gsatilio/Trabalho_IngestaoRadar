
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class RadarList
    {
        [JsonProperty("radar")]
        public List<Radar> Radar { get; set; }
    }
}
