using Models;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Xml.Linq;
internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("File Generator");
        string path = "C:\\5by5\\Estagio\\Semana 7\\Trabalho_Radar\\";
        WriteFilesFromMongo(path);
        WriteFilesFromSQL(path);
    }

    public static void WriteFilesFromMongo(string path)
    {
        var client = MongoDatabase.GetInstance().Connection;
        var db = client.GetDatabase("Radar");
        var collection = db.GetCollection<BsonDocument>("Data");
        var documentos = collection.AsQueryable();

        RadarList radarList = new RadarList();
        radarList.Radar = new List<Radar>();
        foreach (var item in documentos)
        {
            radarList.Radar.Add(new Radar().GenerateRadarByBson(item));
        }

        // Escreve os arquivos de origem Mongo
        WriteToJSON(radarList, path, "MongoFile");
        WriteToXML(radarList, path, "MongoFile");
        WriteToCSV(radarList, path, "MongoFile");
        Console.WriteLine("Escrita do Mongo realizada com sucesso!");
    }

    public static void WriteFilesFromSQL(string path)
    {
        List<Radar> lst = new List<Radar>();
        RadarList radarList = new RadarList();
        var _sql = MsSqlDatabase.GetInstance();
        SqlCommand cmdSQL = new SqlCommand();

        string aux = " SELECT [concessionaria], [ano_do_pnv_snv], [tipo_de_radar], [rodovia], [uf], " +
                     "[km_m], [municipio], [tipo_pista], [sentido], [situacao], " +
                     "[data_da_inativacao], [latitude], [longitude], [velocidade_leve] " +
                     "FROM [dbRadar].[dbo].[RadarData] ";

        _sql.Connection.Open();
        cmdSQL.CommandText = aux;
        cmdSQL.Connection = _sql.Connection;
        try
        {
            using (SqlDataReader reader = cmdSQL.ExecuteReader())
            {
                while (reader.Read())
                {
                    List<string> dataInativacao = new List<string>();

                    var temp = reader.GetString(10).Split(',');
                    foreach (var item in temp)
                    {
                        if (item != "")
                            dataInativacao.Add(item);
                    }

                    Radar obj = new Radar(reader.GetString(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetString(4),
                                      reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9),
                                      dataInativacao, reader.GetString(11), reader.GetString(12), reader.GetInt32(13));

                    lst.Add(obj);
                }
            }
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            _sql.Connection.Close();
            radarList.Radar = lst;
        }

        // Escreve os arquivos de origem SQL
        WriteToJSON(radarList, path, "SQLFile");
        WriteToXML(radarList, path, "SQLFile");
        WriteToCSV(radarList, path, "SQLFile");
        Console.WriteLine("Escrita do SQL realizada com sucesso!");
    }

    public static void WriteToJSON(RadarList radarList, string path, string file)
    {
        string fileName = $"{path}{file}.json";
        File.WriteAllText(fileName, string.Empty);
        var document = JsonConvert.SerializeObject(radarList, Formatting.Indented);
        File.AppendAllLines(fileName, new[] { document });
    }
    public static void WriteToXML(RadarList radarList, string path, string file)
    {
        string fileName = $"{path}{file}.xml";
        File.WriteAllText(fileName, string.Empty);
        string result = "<Root>\n";

        foreach (var item in radarList.Radar)
        {
            result += item.GetXMLDocument() + "\n";
        }
        result += "</Root>";
        File.AppendAllLines(fileName, new[] { result });
    }
    public static void WriteToCSV(RadarList radarList, string path, string file)
    {
        string fileName = $"{path}{file}.csv";
        File.WriteAllText(fileName, string.Empty);
        string result = "concessionaria;ano_do_pnv_snv;tipo_de_radar;rodovia;uf;km_m;municipio;tipo_pista;sentido;situacao;data_da_inativacao;latitude;longitude;velocidade_leve\n";
        foreach (var item in radarList.Radar)
        {
            result += item.GetCSVDocument() + "\n";
        }
        File.AppendAllLines(fileName, new[] { result });
    }
}