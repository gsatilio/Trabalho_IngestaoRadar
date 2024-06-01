using Models;
using MongoDB.Bson;
using System.Data.SqlClient;

internal class Program
{
    private static void Main(string[] args)
    {

        Console.WriteLine("Read and Persist");
        InsertIntoMongo();
    }

    public static void InsertIntoMongo()
    {
        var client = MongoDatabase.GetInstance();
        var db = client.Connection.GetDatabase("Radar");
        var collection = db.GetCollection<BsonDocument>("Data");
        db.DropCollection("Data");

        var listDocument = new List<BsonDocument>();
        foreach (var item in GetSQLRecordsList())
        {
            var document = new BsonDocument { item.GetBsonDocument() };
            listDocument.Add(document);
        }
        collection.InsertMany(listDocument);
    }
    public static List<Radar> GetSQLRecordsList()
    {
        List<Radar> lst = new List<Radar>();
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
                        dataInativacao.Add(item);

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
        _sql.Connection.Close();
        return lst;
    }
}
