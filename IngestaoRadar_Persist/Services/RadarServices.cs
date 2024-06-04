using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using Models;
using MongoDB.Bson.Serialization.Serializers;
using Newtonsoft.Json;
using Repositories;
using System.Net;

namespace Services
{
    public class RadarServices
    {
        private readonly RadarRepository _radarRepository = new();

        public string GetJsonFromHttp(string url)
        {
            try
            {
                var json = string.Empty;
                using (var wc = new WebClient())
                {
                    json = wc.DownloadString(url);
                }
                return json;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return string.Empty;
            }
        }

        public void InsertFileOnSql(string jsonString, int mt, int bt)
        {
            var json = JsonConvert.DeserializeObject<RadarList>(jsonString);
            if (bt == 0)
            {
                // Limpa dados antigos da tabela antes de inserir
                _radarRepository.Delete();
                foreach (var radar in json.Radar)
                {
                    _radarRepository.Insert(radar);
                }
            }
            else
            {
                var batch = json.Radar.Count();
                var runs = 100;
                int batchsize = (batch / runs);
                _radarRepository.Delete();
                _radarRepository.InsertBatch(json.Radar, runs, batchsize, mt);
            }

        }
    }
}
