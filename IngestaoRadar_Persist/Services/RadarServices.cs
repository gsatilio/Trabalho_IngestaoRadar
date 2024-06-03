using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using Models;
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

        public void InsertFileOnSql(string jsonString)
        {
            var json = JsonConvert.DeserializeObject<RadarList>(jsonString);
            // Limpa dados antigos da tabela antes de inserir
            _radarRepository.Delete();
            foreach (var radar in json.Radar)
            {
                _radarRepository.Insert(radar);
            }
        }

        public async Task<bool> InsertFileOnSqlMT(string jsonString)
        {
            try
            {
                var json = JsonConvert.DeserializeObject<RadarList>(jsonString);
                _radarRepository.Delete();
                await _radarRepository.InsertAsync(json.Radar);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
