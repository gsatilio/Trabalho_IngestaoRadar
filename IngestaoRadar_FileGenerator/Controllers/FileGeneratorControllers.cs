using Models;
using Services;
using System.IO;

namespace Controllers
{
    public class FileGeneratorControllers
    {
        private readonly FileGeneratorServices _services = new();

        public bool WriteJsonFromMongo(string path)
        {
            var data = _services.GetRadarListFromMongo();
            return _services.WriteToJsonFile(data, path, "MongoFile");
        }

        public bool WriteXmlFromMongo(string path)
        {
            var data = _services.GetRadarListFromMongo();
            return _services.WriteToXmlFile(data, path, "MongoFile");
        }

        public bool WriteCsvFromMongo(string path)
        {
            var data = _services.GetRadarListFromMongo();
            return _services.WriteToCsvFile(data, path, "MongoFile");
        }

        public bool WriteJsonFromSql(string path)
        {
            var data = _services.GetRadarListFromSql();
            return _services.WriteToJsonFile(data, path, "SQLFile");
        }

        public bool WriteXmlFromSql(string path)
        {
            var data = _services.GetRadarListFromSql();
            return _services.WriteToXmlFile(data, path, "SQLFile");
        }

        public bool WriteCsvFromSql(string path)
        {
            var data = _services.GetRadarListFromSql();
            return _services.WriteToCsvFile(data, path, "SQLFile");
        }
    }
}
