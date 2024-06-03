using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    // Classe criada seguindo os padrões do Proxy Cache Pattern.
    public class CacheRepositoriesProxy : IFileGeneratorRepositories
    {
        private readonly IFileGeneratorRepositories _repositories;
        private List<Radar>? _mongoCachedList;
        private List<Radar>? _sqlCachedList;

        public CacheRepositoriesProxy(IFileGeneratorRepositories repositories)
        {
            Console.WriteLine("Instancia criada");
            _repositories = repositories;
            _mongoCachedList = null;
            _sqlCachedList = null;
        }

        public List<Radar> SelectAllMongo()
        {
            if (_mongoCachedList == null)
            {
                Console.WriteLine("Querie no Mongo executada!");
                _mongoCachedList = _repositories.SelectAllMongo();
            }
            else
            {
                Console.WriteLine($"Cache utilizado: {_mongoCachedList.Count} registros");
            }
            return _mongoCachedList;
        }

        public List<Radar> SelectAllSql()
        {
            if (_sqlCachedList == null)
            {
                Console.WriteLine("Querie no SQL executada!");
                _sqlCachedList = _repositories.SelectAllSql();
            }
            else
            {
                Console.WriteLine($"Cache utilizado: {_sqlCachedList.Count} registros");
            }
            return _sqlCachedList;
        }
    }
}
