using System.Data.SqlClient;

namespace Models
{
    public sealed class MsSqlDatabase
    {
        private static MsSqlDatabase _instance;
        private readonly string _connectionString = "Data Source=127.0.0.1; Initial Catalog=dbRadar; User Id=SA; Password=SqlServer2019!;TrustServerCertificate=True";
        public SqlConnection Connection;

        //private MsSqlDatabase()
        public MsSqlDatabase()
        {
            Connection = new SqlConnection()
            {
                ConnectionString = _connectionString
            };
        }

        public static MsSqlDatabase GetInstance()
        {
            if (_instance == null)
            {
                _instance = new MsSqlDatabase();
            }

            return _instance;
        }
    }
}
