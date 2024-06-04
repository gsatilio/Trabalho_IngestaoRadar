using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Services;
using System.Net;
namespace Controllers
{
    public class RadarController
    {
        private RadarServices _services = new();
        
        public bool SaveRadarDataFromApi(string url, int mt, int bt)
        {
            var response = false;
            try
            {
                var json = _services.GetJsonFromHttp(url);
                _services.InsertFileOnSql(json, mt, bt);
                response = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                response = false;
            }
            return response;
        }
     
        public bool SaveRadarDataFromFile(string path, int mt, int bt)
        {
            var response = false;
            try
            {
                var reader = new StreamReader(path);
                var json = reader.ReadToEnd();
                // Tenta converter o arquivo Json para objeto a fim de verificar sua validade
                JToken.Parse(json);

                _services.InsertFileOnSql(json, mt, bt);
                response = true;
            }
            catch (JsonReaderException jsonReaderException)
            {
                Console.WriteLine("JSON_PARSE_ERROR: " + jsonReaderException.Message);
                response = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                response = false;
            }
            return response;
        }
    }
}
