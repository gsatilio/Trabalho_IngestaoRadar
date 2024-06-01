using Models;
using MongoDB.Bson;
using System.Data.SqlClient;
Console.WriteLine("Read and Persist");

var client = new MongoDatabase().Connection;
var db = client.GetDatabase("Radar");
var collection = db.GetCollection<BsonDocument>("Data");
db.DropCollection("Data");

var listDocument = new List<BsonDocument>();

foreach (var item in GetSQLRecordsList())
{
    var document = new BsonDocument{
                            {"concessionaria", item.Concessionaria },
                            {"ano_do_pnv_snv", item.Ano_do_pnv_snv },
                            {"tipo_de_radar", item.Tipo_de_radar },
                            {"rodovia", item.Rodovia },
                            {"uf", item.Uf },
                            {"km_m", item.Km_m },
                            {"municipio", item.Municipio },
                            {"tipo_pista", item.Tipo_pista },
                            {"sentido", item.Sentido },
                            //{"data_da_inativacao", item.Data_da_inativacao },
                            {"latitude", item.Latitude },
                            {"longitude", item.Longitude },
                            {"velocidade_leve", item.Velocidade_leve }
                };
    listDocument.Add(document);
}
collection.InsertMany(listDocument);

List<Radar> GetSQLRecordsList()
{
    List<Radar> lst = new List<Radar>();
    var _connSQL = new MsSqlDatabase().Connection;
    SqlCommand cmdSQL = new SqlCommand();
    string[] auxArray = new string[1];

    string aux = " SELECT [concessionaria], [ano_do_pnv_snv], [tipo_de_radar], [rodovia], [uf], " +
                 "[km_m], [municipio], [tipo_pista], [sentido], [situacao], " +
                 "[data_da_inativacao], [latitude], [longitude], [velocidade_leve] " +
                 "FROM [dbRadar].[dbo].[RadarData] ";

    _connSQL.Open();
    cmdSQL.CommandText = aux;
    cmdSQL.Connection = _connSQL;
    try
    {
        using (SqlDataReader dr = cmdSQL.ExecuteReader())
        {
            while (dr.Read())
            {
                auxArray[0] = dr.GetString(10);
                lst.Add(new Radar(dr.GetString(0), dr.GetInt32(1), dr.GetString(2), dr.GetString(3), dr.GetString(4),
                                  dr.GetString(5), dr.GetString(6), dr.GetString(7), dr.GetString(8), dr.GetString(9),
                                  auxArray, dr.GetString(11), dr.GetString(12), dr.GetInt32(13)));
            }
        }
    }
    catch (SqlException)
    {
        throw;
    }
    _connSQL.Close();
    return lst;
}