using MongoDB.Bson;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace Models
{
    public class Radar
    {
        [JsonProperty("concessionaria")]
        private string _concessionaria { get; set; }
        [JsonProperty("ano_do_pnv_snv")]
        private int _ano_do_pnv_snv { get; set; }
        [JsonProperty("tipo_de_radar")]
        private string _tipo_de_radar { get; set; }
        [JsonProperty("rodovia")]
        private string _rodovia { get; set; }
        [JsonProperty("uf")]
        private string _uf { get; set; }
        [JsonProperty("km_m")]
        private string _km_m { get; set; }
        [JsonProperty("municipio")]
        private string _municipio { get; set; }
        [JsonProperty("tipo_pista")]
        private string _tipo_pista { get; set; }
        [JsonProperty("sentido")]
        private string _sentido { get; set; }
        [JsonProperty("situacao")]
        private string _situacao { get; set; }
        [JsonProperty("data_da_inativacao")]
        private List<string> _data_da_inativacao { get; set; }
        [JsonProperty("latitude")]
        private string _latitude { get; set; }
        [JsonProperty("longitude")]
        private string _longitude { get; set; }
        [JsonProperty("velocidade_leve")]
        private int _velocidade_leve { get; set; }

        public Radar()
        {
            
        }

        public Radar(
            string concessionaria,
            int anoDoPnvSnv,
            string tipoDeRadar,
            string rodovia,
            string uf,
            string kmM,
            string municipio,
            string tipoPista,
            string sentido,
            string situacao,
            List<string> dataDaInativacao,
            string latitude,
            string longitude,
            int velocidadeLeve) 
        {
            _concessionaria = concessionaria;
            _ano_do_pnv_snv = anoDoPnvSnv;
            _tipo_de_radar = tipoDeRadar;
            _rodovia = rodovia;
            _uf = uf;
            _km_m = kmM;
            _municipio = municipio;
            _tipo_pista = tipoPista;
            _sentido = sentido;
            _situacao = situacao;
            _data_da_inativacao = dataDaInativacao;
            _latitude = latitude;
            _longitude = longitude;
            _velocidade_leve = velocidadeLeve;
        }


        public Radar GenerateRadarByBson (BsonDocument bsonItem)
        {
            // aqui era um construtor normal mas deu conflito com o DeserializeObject do Persist -> RadarServices, fiz uma funcao pra gerar o objeto
            Radar newRadar = new Radar ();
            List<string> tempList = new List<string>();
            var temp = bsonItem["data_da_inativacao"].AsBsonArray.Select(p => p.AsString);
            foreach (var aux in temp)
            {
                if (aux != "")
                    tempList.Add(aux);
            }
            newRadar._concessionaria = (string)bsonItem["concessionaria", null];
            newRadar._ano_do_pnv_snv = (int)bsonItem["ano_do_pnv_snv", null];
            newRadar._tipo_de_radar = (string)bsonItem["tipo_de_radar", null];
            newRadar._rodovia = (string)bsonItem["rodovia", null];
            newRadar._uf = (string)bsonItem["uf", null];
            newRadar._km_m = (string)bsonItem["km_m", null];
            newRadar._municipio = (string)bsonItem["municipio", null];
            newRadar._tipo_pista = (string)bsonItem["tipo_pista", null];
            newRadar._sentido = (string)bsonItem["sentido", null];
            newRadar._situacao = (string)bsonItem["situacao", null];
            newRadar._data_da_inativacao = tempList;
            newRadar._latitude = (string)bsonItem["latitude", null];
            newRadar._longitude = (string)bsonItem["longitude", null];
            newRadar._velocidade_leve = (int)bsonItem["velocidade_leve", null];

            return newRadar;
        }

        public override string ToString()
        {
            return $"Coordenadas = {_latitude} : {_longitude}";
        }

        public string ToSQL()
        {
            string arrayData = "";
            _data_da_inativacao.ForEach(s => arrayData += s + ",");

            return
                $"INSERT INTO RadarData VALUES (" +
                $"'{_concessionaria}'," +
                $"'{_ano_do_pnv_snv}'," +
                $"'{_tipo_de_radar}'," +
                $"'{_rodovia}'," +
                $"'{_uf}'," +
                $"'{_km_m}'," +
                $"'{_municipio}'," +
                $"'{_tipo_pista}'," +
                $"'{_sentido}'," +
                $"'{_situacao}'," +
                $"'{arrayData}'," +
                $"'{_latitude}'," +
                $"'{_longitude}'," +
                $"'{_velocidade_leve}'" +
                $")";
        }

        public BsonDocument GetBsonDocument()
        {
            return new BsonDocument{
                            {"concessionaria", this._concessionaria },
                            {"ano_do_pnv_snv", this._ano_do_pnv_snv },
                            {"tipo_de_radar", this._tipo_de_radar },
                            {"rodovia", this._rodovia },
                            {"uf", this._uf },
                            {"km_m", this._km_m },
                            {"municipio", this._municipio },
                            {"tipo_pista", this._tipo_pista },
                            {"sentido", this._sentido },
                            {"situacao", this._situacao },
                            {"data_da_inativacao", new BsonArray(this._data_da_inativacao) },
                            {"latitude", this._latitude },
                            {"longitude", this._longitude },
                            {"velocidade_leve", this._velocidade_leve }
                };
        }
        public string? GetXMLDocument()
        {
            string arrayData = "";
            _data_da_inativacao.ForEach(s => arrayData += s + ",");
            var penaltieApplied =
                    new XElement("radar",
                    new XElement("concessionaria", this._concessionaria),
                    new XElement("ano_do_pnv_snv", this._ano_do_pnv_snv),
                    new XElement("tipo_de_radar", this._tipo_de_radar),
                    new XElement("rodovia", this._rodovia),
                    new XElement("uf", this._uf),
                    new XElement("km_m", this._km_m),
                    new XElement("municipio", this._municipio),
                    new XElement("tipo_pista", this._tipo_pista),
                    new XElement("sentido", this._sentido),
                    new XElement("data_da_inativacao", arrayData),
                    new XElement("latitude", this._latitude),
                    new XElement("longitude", this._longitude),
                    new XElement("velocidade_leve", this._velocidade_leve)
                    );
            return penaltieApplied.ToString();
        }
        public string? GetCSVDocument()
        {
            string arrayData = "";
            _data_da_inativacao.ForEach(s => arrayData += s + ",");
            string result = "";
            result += $"{_concessionaria};{_ano_do_pnv_snv};{_tipo_de_radar};{_rodovia};{_uf};{_km_m};{_municipio};{_tipo_pista};{_sentido};{_situacao};{arrayData};{_latitude};{_longitude};{_velocidade_leve}";
            return result;
        }
    }
}
