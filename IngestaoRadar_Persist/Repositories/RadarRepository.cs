using Models;
using MongoDB.Driver.Core.Misc;
using System.Data.SqlClient;
using System.Threading;
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

        public bool InsertManyMT(List<Radar> lst, int runs, int batchsize)
        {
            var result = false;
            try
            {
                // Aqui eu crio uma tupla pra salvar a string e o Id (i)
                // depois eu controlo se vou pegar os Ids pares ou Ids impares
                List<Tuple<string, int>> listInsert = new List<Tuple<string, int>>();
                for (int i = 0; i <= batchsize; i++)
                {
                    var tc = "INSERT INTO RadarData VALUES";
                    foreach (var radar in lst.Skip(runs * i).Take(runs))
                    {
                        var datesCsv = string.Empty;
                        if (radar.DataDaInativacao.Count > 0)
                        {
                            radar.DataDaInativacao.ForEach(s => datesCsv += s + ",");
                        }
                        tc += $"('{radar.Concessionaria}', {radar.AnoDoPnvSnv}, '{radar.TipoDeRadar}', '{radar.Rodovia}', '{radar.Uf}', '{radar.KmM}'," +
                              $"'{radar.Municipio}', '{radar.TipoPista}','{radar.Sentido}','{radar.Situacao}','{datesCsv}'," +
                              $"'{radar.Latitude}', '{radar.Longitude}', {radar.VelocidadeLeve}),";
                    }
                    tc = tc.Substring(0, tc.Length - 1);
                    Tuple<string, int> tupla = new Tuple<string, int>(tc, i);
                    listInsert.Add(tupla);
                }

                // crio dois threads, cada um executa a mesma funcao, mas um com parametro de Pares e outro de Impares
                Thread T1 = new Thread(() => StartInsert(listInsert, 0));
                Thread T2 = new Thread(() => StartInsert(listInsert, 1));

                // starto os dois threads e aguardo eles terminarem
                T1.Start();
                T2.Start();
                T1.Join();
                T2.Join();

                void StartInsert(List<Tuple<string, int>> listInsert, double tipo)
                {
                    // Estava dando erro porque o MsSqlDatabase retorna a mesma instancia depois de aberta, dando conflito
                    // ou seja, estou criando uma instancia nova pra cada thread
                    var db = new SqlConnection();
                    var cmd = new SqlCommand();
                    cmd.Connection = db;
                    db.ConnectionString = "Data Source=127.0.0.1; Initial Catalog=dbRadar; User Id=SA; Password=SqlServer2019!;TrustServerCertificate=True";
                    db.Open();

                    if (tipo == 0) // apenas registros onde a tupla é PAR
                    {
                        foreach (var itm in listInsert.Where(n => n.Item2 % 2 == 0))
                        {
                            cmd.CommandText = itm.Item1;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else // tupla IMPAR
                    {
                        foreach (var itm in listInsert.Where(n => n.Item2 % 2 != 0))
                        {

                            cmd.CommandText = itm.Item1;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    cmd.Connection.Close();
                }
                result = true;
            }
            catch
            {
                throw;

            }
            finally
            {
                _sql.Connection.Close();
            }
            return result;
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
