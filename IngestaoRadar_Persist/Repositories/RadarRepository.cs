using Models;
using System.Data.SqlClient;
namespace Repositories
{
    public class RadarRepository
    {
        private readonly MsSqlDatabase _sql;
        
        public RadarRepository()
        {
            _sql = MsSqlDatabase.GetInstance();
            _sql.Connection.Open();
        }

        public bool Insert(Radar radar)
        {
            var result = false;
            try
            {
                var cmd = new SqlCommand
                {
                    Connection = _sql.Connection,
                    CommandText = Radar.InsertSql
                };

                var datesCsv = "";
                radar.DataDaInativacao.ForEach(s => datesCsv += s + ",");
                cmd.Parameters.Add(new SqlParameter("@Concessionaria", radar.Concessionaria));
                cmd.Parameters.Add(new SqlParameter("@AnoDoPnvSnv", radar.AnoDoPnvSnv));
                cmd.Parameters.Add(new SqlParameter("@TipoDeRadar", radar.TipoDeRadar));
                cmd.Parameters.Add(new SqlParameter("@Rodovia", radar.Rodovia));
                cmd.Parameters.Add(new SqlParameter("@Uf", radar.Uf));
                cmd.Parameters.Add(new SqlParameter("@KmM", radar.KmM));
                cmd.Parameters.Add(new SqlParameter("@Municipio", radar.Municipio));
                cmd.Parameters.Add(new SqlParameter("@TipoPista", radar.TipoPista));
                cmd.Parameters.Add(new SqlParameter("@Sentido", radar.Sentido));
                cmd.Parameters.Add(new SqlParameter("@Situacao", radar.Situacao));
                cmd.Parameters.Add(new SqlParameter("@DataInativacao", datesCsv));
                cmd.Parameters.Add(new SqlParameter("@Latitude", radar.Latitude));
                cmd.Parameters.Add(new SqlParameter("@Longitude", radar.Longitude));
                cmd.Parameters.Add(new SqlParameter("@VelocidadeLeve", radar.VelocidadeLeve));

                cmd.ExecuteNonQuery();
                result = true;
            }
            catch (SqlException sqlException)
            {
                Console.WriteLine("SQL ERROR: " + sqlException.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("SYSTEM ERROR: " + ex);
                throw;
            }
            finally
            {
                _sql.Connection.Close();
            }
            return result;
        }
    }
}
