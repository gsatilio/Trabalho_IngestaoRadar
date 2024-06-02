using MongoDB.Bson;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace Models
{
    public class Radar
    {
        [JsonProperty("concessionaria")]
        public string Concessionaria { get; set; }
        [JsonProperty("ano_do_pnv_snv")]
        public int AnoDoPnvSnv { get; set; }
        [JsonProperty("tipo_de_radar")]
        public string TipoDeRadar { get; set; }
        [JsonProperty("rodovia")]
        public string Rodovia { get; set; }
        [JsonProperty("uf")]
        public string Uf { get; set; }
        [JsonProperty("km_m")]
        public string KmM { get; set; }
        [JsonProperty("municipio")]
        public string Municipio { get; set; }
        [JsonProperty("tipo_pista")]
        public string TipoPista { get; set; }
        [JsonProperty("sentido")]
        public string Sentido { get; set; }
        [JsonProperty("situacao")]
        public string Situacao { get; set; }
        [JsonProperty("data_da_inativacao")]
        public List<string> DataDaInativacao { get; set; }
        [JsonProperty("latitude")]
        public string Latitude { get; set; }
        [JsonProperty("longitude")]
        public string Longitude { get; set; }
        [JsonProperty("velocidade_leve")]
        public int VelocidadeLeve { get; set; }
        public static readonly string InsertSql = "INSERT INTO RadarData VALUES (@Concessionaria, @AnoDoPnvSnv, @TipoDeRadar, @Rodovia, @Uf, " +
                                           "@KmM, @Municipio, @TipoPista, @Sentido, @Situacao, @DataInativacao, @Latitude, @Longitude, @VelocidadeLeve)";
        public static readonly string SelectSql = " SELECT [concessionaria], [ano_do_pnv_snv], [tipo_de_radar], [rodovia], [uf], [km_m], [municipio], [tipo_pista], " +
                                           "[sentido], [situacao], [data_da_inativacao], [latitude], [longitude], [velocidade_leve] " +
                                           "FROM [dbRadar].[dbo].[RadarData] ";

        public override string ToString()
        {
            return $"Coordenadas = {Latitude} : {Longitude}";
        }

        public Radar GenerateObjectByBson (BsonDocument document)
        {
            var datesList = new List<string>();
            var bsonArray = document.GetValue("data_da_inativacao").AsBsonArray.Select(p => p.AsString);
            foreach (var item in bsonArray)
            {
                if (item != "")
                    datesList.Add(item);
            }
            return new Radar
            {
                Concessionaria = (string)document.GetValue("concessionaria", null),
                AnoDoPnvSnv = (int)document.GetValue("ano_do_pnv_snv", null),
                TipoDeRadar = (string)document.GetValue("tipo_de_radar", null),
                Rodovia = (string)document.GetValue("rodovia", null),
                Uf = (string)document.GetValue("uf", null),
                KmM = (string)document.GetValue("km_m", null),
                Municipio = (string)document.GetValue("municipio", null),
                TipoPista = (string)document.GetValue("tipo_pista", null),
                Sentido = (string)document.GetValue("sentido", null),
                Situacao = (string)document.GetValue("situacao", null),
                DataDaInativacao = datesList,
                Latitude = (string)document.GetValue("latitude", null),
                Longitude = (string)document.GetValue("longitude", null),
                VelocidadeLeve = (int)document.GetValue("velocidade_leve", null),
            };
        }

        public BsonDocument GetBsonDocument()
        {
            return new BsonDocument{
                            {"concessionaria", Concessionaria },
                            {"ano_do_pnv_snv", AnoDoPnvSnv },
                            {"tipo_de_radar", TipoDeRadar },
                            {"rodovia", Rodovia },
                            {"uf", Uf },
                            {"km_m", KmM },
                            {"municipio", Municipio },
                            {"tipo_pista", TipoPista },
                            {"sentido", Sentido },
                            {"situacao", Situacao },
                            {"data_da_inativacao", new BsonArray(DataDaInativacao) },
                            {"latitude", Latitude },
                            {"longitude", Longitude },
                            {"velocidade_leve", VelocidadeLeve }
                };
        }

        public string? GetXMLDocument()
        {
            var arrayData = "";
            DataDaInativacao.ForEach(s => arrayData += s + ",");
            var xml =
                    new XElement("radar",
                    new XElement("concessionaria", Concessionaria),
                    new XElement("ano_do_pnv_snv", AnoDoPnvSnv),
                    new XElement("tipo_de_radar", TipoDeRadar),
                    new XElement("rodovia", Rodovia),
                    new XElement("uf", Uf),
                    new XElement("km_m", KmM),
                    new XElement("municipio", Municipio),
                    new XElement("tipo_pista", TipoPista),
                    new XElement("sentido", Sentido),
                    new XElement("data_da_inativacao", arrayData),
                    new XElement("latitude", Latitude),
                    new XElement("longitude", Longitude),
                    new XElement("velocidade_leve", VelocidadeLeve)
                    );
            return xml.ToString();
        }

        public string? GetCSVData()
        {
            var arrayData = "";
            DataDaInativacao.ForEach(s => arrayData += s + ",");
            string result = "";
            result += $"{Concessionaria.Replace(',', '.')}," +
                      $"{AnoDoPnvSnv}," +
                      $"{TipoDeRadar.Replace(',', '.')}," +
                      $"{Rodovia.Replace(',', '.')}," +
                      $"{Uf.Replace(',', '.')}," +
                      $"{KmM.Replace(',', '.')}," +
                      $"{Municipio.Replace(',', '.')}," +
                      $"{TipoPista.Replace(',', '.')}," +
                      $"{Sentido.Replace(',', '.')}," +
                      $"{Situacao.Replace(',', '.')}," +
                      $"{arrayData.Replace(',', '.')}," +
                      $"{Latitude.Replace(',', '.')}," +
                      $"{Longitude.Replace(',', '.')}," +
                      $"{VelocidadeLeve}";
            return result;
        }
    }
}
