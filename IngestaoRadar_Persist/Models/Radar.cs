using Newtonsoft.Json;
using System.Text.Json.Serialization;

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
        private string[] _data_da_inativacao { get; set; }
        [JsonProperty("latitude")]
        private string _latitude { get; set; }
        [JsonProperty("longitude")]
        private string _longitude { get; set; }
        [JsonProperty("velocidade_leve")]
        private int velocidade_leve { get; set; }

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
            velocidade_leve = velocidadeLeve;
        }

        public override string ToString()
        { 
            return $"Coordenadas = {_latitude} : {_longitude}";
        }
    }
}
