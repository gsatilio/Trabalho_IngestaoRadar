using System.Data.SqlClient;
using Models;
using System.Text.Json;
using MongoDB.Bson;
using Repositories;

namespace Services
{
    public class ReadAndPersistServices
    {
        private readonly ReadAndPersistRepositories _repositories = new();

        public bool MigrateDataFromSqlToMongo()
        {
            var result = false;
            try
            {
                // Read from SQL
                var sqlData = _repositories.SelectAllSql();

                // Convert to Bson
                var listDocument = new List<BsonDocument>();
                foreach (var item in sqlData)
                {
                    var document = new BsonDocument
                    {
                        item.GetBsonDocument()
                    };
                    listDocument.Add(document);
                }

                // Write in Mongo
                _repositories.InsertManyMongoDb(listDocument);

                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }
    }
}
