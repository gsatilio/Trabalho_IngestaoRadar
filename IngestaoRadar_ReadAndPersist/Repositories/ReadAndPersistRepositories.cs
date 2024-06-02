using System.Data.SqlClient;
using Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Repositories
{
    public class ReadAndPersistRepositories
    {
        private readonly MongoDatabase _mongo = MongoDatabase.GetInstance();
        private readonly MsSqlDatabase _sql = MsSqlDatabase.GetInstance();

        public List<Radar> SelectAllSql()
        {
            var radars = new List<Radar>();
            try
            {
                _sql.Connection.Open();
                var cmd = new SqlCommand
                {
                    CommandText = Radar.SelectSql,
                    Connection = _sql.Connection
                };
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var dataInativacao = new List<string>();
                        foreach (var item in reader.GetString(10).Split(','))
                        {
                            dataInativacao.Add(item);
                        }

                        radars.Add(new Radar
                        {
                            Concessionaria = reader.GetString(0),
                            AnoDoPnvSnv = reader.GetInt32(1),
                            TipoDeRadar = reader.GetString(2),
                            Rodovia = reader.GetString(3),
                            Uf = reader.GetString(4),
                            KmM = reader.GetString(5),
                            Municipio = reader.GetString(6),
                            TipoPista = reader.GetString(7),
                            Sentido = reader.GetString(8),
                            Situacao = reader.GetString(9),
                            DataDaInativacao = dataInativacao,
                            Latitude = reader.GetString(11),
                            Longitude = reader.GetString(12),
                            VelocidadeLeve = reader.GetInt32(13)
                        }
                        );
                    }
                }
            }
            catch (SqlException sqlException)
            {
                Console.WriteLine("SQL ERROR: " + sqlException);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("SYSTEM ERROR: " + ex);
                throw;
            }
            _sql.Connection.Close();
            return radars;
        }

        public void InsertManyMongoDb(List<BsonDocument> documentList)
        {
            try
            {
                var database = _mongo.Connection.GetDatabase("Radar");
                var collection = database.GetCollection<BsonDocument>("Data");
                database.DropCollection("Data");
                collection.InsertMany(documentList);
            }
            catch (MongoException mongoException)
            {
                Console.WriteLine("MongoDb ERROR: " + mongoException);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("System ERROR: " + ex);
                throw;
            }
        }
    }
}
