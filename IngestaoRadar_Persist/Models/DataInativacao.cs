using Newtonsoft.Json;
namespace Models
{
    [JsonArray]
    public class DataInativacao
    {
        public string Data { get; set; }

        public DataInativacao(string data)
        {
            Data = data;
        }
    }
}
