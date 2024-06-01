using Models;
using System.Data.SqlClient;
namespace Repositories
{
    public class RadarRepository
    {
        private MsSqlDatabase _db;
        public RadarRepository()
        {
            _db = MsSqlDatabase.GetInstance();
            _db.Connection.Open();
        }
        public bool Insert(Radar radar)
        {
            bool result = false;
            try
            {
                SqlCommand cmd = new SqlCommand(radar.ToSQL(), _db.Connection);
                cmd.ExecuteNonQuery();
                result = true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _db.Connection.Close();
            }
            return result;
        }
    }
}
