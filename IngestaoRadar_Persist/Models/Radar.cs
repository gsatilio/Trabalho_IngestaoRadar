using MongoDB.Bson;
using Newtonsoft.Json;

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
                $"'{_velocidade_leve}'"+
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
                            {"data_da_inativacao", new BsonArray(this._data_da_inativacao) },
                            {"latitude", this._latitude },
                            {"longitude", this._longitude },
                            {"velocidade_leve", this._velocidade_leve }
                };
        }
    }
}
