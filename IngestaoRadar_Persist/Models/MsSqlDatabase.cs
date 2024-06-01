using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public sealed class MsSqlDatabase
    {
        private static MsSqlDatabase _instance;
        private readonly string _connectionString = "";
        public SqlConnection Connection;

        private MsSqlDatabase()
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
