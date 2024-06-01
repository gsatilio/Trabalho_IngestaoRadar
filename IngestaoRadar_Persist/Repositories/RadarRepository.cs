using Models;
using System.Data.SqlClient;
namespace Repositories
{
    public class RadarRepository
    {
        private SqlConnection _conn;
        public RadarRepository()
        {
            _conn = new MsSqlDatabase().Connection;
            _conn.Open();
        }
        public bool Insert(Radar radar)
        {
            bool result = false;
            try
            {
                SqlCommand cmd = new SqlCommand(radar.ToSQL(), _conn);
                cmd.ExecuteNonQuery();
                result = true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _conn.Close();
            }
            return result;
        }
    }
}
