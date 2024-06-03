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
        }

        public bool Insert(Radar radar)
        {
            var result = false;
            try
            {
               _sql.Connection.Open();
                var cmd = new SqlCommand
                {
                    Connection = _sql.Connection,
                    CommandText = Radar.InsertSql
                };

                var datesCsv = string.Empty;
                if (radar.DataDaInativacao.Count > 0)
                {
                    radar.DataDaInativacao.ForEach(s => datesCsv += s + ",");
                }
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

        private void InsertOne(Radar radar)
        {
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
        }

        public async Task InsertAsync(List<Radar> radar)
        {
            int numberOfThreads = 3;
            Task[] tasks = new Task[numberOfThreads];

            _sql.Connection.Open();
            //for (int i = 0; i < numberOfThreads; i++)
            //{
            //    tasks[i] = Task.Run(() => InsertOne(radar));
            //}

            var taskController = 1;
            foreach (var item in radar)
            {
                tasks[taskController] = Task.Run(() => InsertOne(item));
                
                taskController++;
                if (taskController > numberOfThreads)
                {
                    taskController = 1;
                }
            }

            await Task.WhenAll(tasks);
            _sql.Connection.Close();
        }

        public bool Delete()
        {
            var result = false;
            try
            {
                _sql.Connection.Open();
                var cmd = new SqlCommand
                {
                    Connection = _sql.Connection,
                    CommandText = Radar.DeleteSql
                };
                cmd.ExecuteNonQuery();
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                result = false;
            }
            finally
            {
                _sql.Connection.Close();
            }
            return result;
        }
    }
}
