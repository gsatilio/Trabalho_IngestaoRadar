using MongoDB.Driver;

namespace Models
{
    // Classe criada seguindo os padrões do Singleton Pattern.
    public sealed class MongoDatabase
    {
        private readonly string _connectionString = "mongodb://root:Mongo%402024%23@localhost:27017/";
        private static MongoDatabase _instance;
        public MongoClient Connection;

        private MongoDatabase()
        {
            Connection = new MongoClient(_connectionString);
        }
        public static MongoDatabase GetInstance()
        {
            if (_instance == null)
            {
                _instance = new MongoDatabase();
            }

            return _instance;
        }
    }
}
