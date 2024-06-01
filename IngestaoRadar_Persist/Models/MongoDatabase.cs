using MongoDB.Driver;

namespace Models
{
    public sealed class MongoDatabase
    {
        private static IMongoDatabase _instance;
        private readonly string _connectionString = "mongodb://root:Mongo%402024%23@localhost:27017/";
        public MongoClient Connection;

        //private MsSqlDatabase()
        public MongoDatabase()
        {
            Connection = new MongoClient(_connectionString);
        }
    }
}
