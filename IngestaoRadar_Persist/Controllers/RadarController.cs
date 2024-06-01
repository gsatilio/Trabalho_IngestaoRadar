using Services;
using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Controllers
{
    public class RadarController
    {
        private RadarServices _services = new();
        
        public bool SaveRadarDataFromApi(string url)
        {
            var response = false;
            try
            {
                string json = string.Empty;
                using (var wc = new WebClient())
                {
                    json = wc.DownloadString(url);
                }

                _services.InsertFileOnSQL(json);
                response = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                response = false;
            }
            return response;
        } 
    }
}
