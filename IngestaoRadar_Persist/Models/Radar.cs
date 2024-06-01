using Newtonsoft.Json;

namespace Models
{
    public class Radar
    {
        /*
         "radar": [
               {
                   "concessionaria": "AUTOPISTA FERNÃO DIAS",
                   "ano_do_pnv_snv": "2007",
                   "tipo_de_radar": "Controlador",
                   "rodovia": "BR-381",
                   "uf": "MG",
                   "km_m": "483,700",
                   "municipio": "Betim",
                   "tipo_pista": "Principal",
                   "sentido": "Crescente",
                   "situacao": "Ativo",
                   "data_da_inativacao": [],
                   "latitude": "-19,959486",
                   "longitude": "-44,085386",
                   "velocidade_leve": "80"
               },
         */
        [JsonProperty("concessionaria")]
        private string _concessionaria { get; set; }
        public string Concessionaria { get { return _concessionaria; } }
        [JsonProperty("ano_do_pnv_snv")]
        private int _ano_do_pnv_snv { get; set; }
        public int Ano_do_pnv_snv { get { return _ano_do_pnv_snv; } }
        [JsonProperty("tipo_de_radar")]
        private string _tipo_de_radar { get; set; }
        public string Tipo_de_radar { get { return _tipo_de_radar; } }
        [JsonProperty("rodovia")]
        private string _rodovia { get; set; }
        public string Rodovia { get { return _rodovia; } }
        [JsonProperty("uf")]
        private string _uf { get; set; }
        public string Uf { get { return _uf; } }
        [JsonProperty("km_m")]
        private string _km_m { get; set; }
        public string Km_m { get { return _km_m; } }
        [JsonProperty("municipio")]
        private string _municipio { get; set; }
        public string Municipio { get { return _municipio; } }
        [JsonProperty("tipo_pista")]
        private string _tipo_pista { get; set; }
        public string Tipo_pista { get { return _tipo_pista; } }
        [JsonProperty("sentido")]
        private string _sentido { get; set; }
        public string Sentido { get { return _sentido; } }
        [JsonProperty("situacao")]
        private string _situacao { get; set; }
        public string Situacao {  get { return _situacao; } }
        [JsonProperty("data_da_inativacao")]
        private string[] _data_da_inativacao { get; set; }
        public string[] Data_da_inativacao { get { return _data_da_inativacao; } }
        [JsonProperty("latitude")]
        private string _latitude { get; set; }
        public string Latitude { get { return _latitude; } }
        [JsonProperty("longitude")]
        private string _longitude { get; set; }
        public string Longitude { get { return _longitude; } }
        [JsonProperty("velocidade_leve")]
        private int _velocidade_leve { get; set; }
        public int Velocidade_leve { get { return _velocidade_leve; } }

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
            string[] dataDaInativacao, 
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
                //$"'{_data_da_inativacao},'" +
                $"''," +
                $"'{_latitude}'," +
                $"'{_longitude}'," +
                $"'{_velocidade_leve}'"+
                $")";
        }
    }
}
