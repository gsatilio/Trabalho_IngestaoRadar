﻿using Models;
using Newtonsoft.Json;
using Repositories;

namespace Services
{
    public class FileGeneratorServices
    {
        private readonly FileGeneratorRepositories _repositories = new();

        public List<Radar> GetRadarListFromMongo()
        {
            return _repositories.SelectAllMongo();
        }

        public List<Radar> GetRadarListFromSql()
        {
            return _repositories.SelectAllSql();
        }

        public bool WriteToJsonFile(List<Radar> radars, string path, string file)
        {
            var result = false;
            try
            {
                var radarList = new RadarList
                {
                    Radar = radars
                };
                var fileName = $"{path}{file}.json";

                // Conversão
                var content = JsonConvert.SerializeObject(radarList, Formatting.Indented);

                // Escrita
                File.WriteAllText(fileName, string.Empty);
                File.AppendAllLines(fileName, new[] { content });
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        public bool WriteToXmlFile(List<Radar> radars, string path, string file)
        {
            var result = false;

            try
            {
                var radarList = new RadarList
                {
                    Radar = radars
                };
                var fileName = $"{path}{file}.xml";

                // Conversão
                var content = "<Root>\n";
                foreach (var radar in radarList.Radar)
                {
                    content += radar.GetXMLDocument() + "\n";
                }
                content += "</Root>";

                // Escrita
                File.WriteAllText(fileName, string.Empty);
                File.AppendAllLines(fileName, new[] { content });
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        public bool WriteToCsvFile(List<Radar> radars, string path, string file)
        {
            var result = false;
            try
            {
                var radarList = new RadarList
                {
                    Radar = radars
                };
                var fileName = $"{path}{file}.csv";

                // Conversão
                var header = "concessionaria,ano_do_pnv_snv,tipo_de_radar,rodovia,uf,km_m,municipio,tipo_pista,sentido,situacao,data_da_inativacao,latitude,longitude,velocidade_leve\n";
                var content = header;
                foreach (var radar in radarList.Radar)
                {
                    content += radar.GetCSVData() + "\n";
                }

                // Escrita
                File.WriteAllText(fileName, string.Empty);
                File.AppendAllLines(fileName, new[] { content });
                
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
    }
}
