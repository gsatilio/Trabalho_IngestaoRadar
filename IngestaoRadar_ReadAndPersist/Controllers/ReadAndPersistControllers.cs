using Services;

namespace Controllers
{
    public class ReadAndPersistControllers
    {
        private readonly ReadAndPersistServices _services = new();

        public bool MigrateData()
        {
            return _services.MigrateDataFromSqlToMongo();
        }
    }
}
