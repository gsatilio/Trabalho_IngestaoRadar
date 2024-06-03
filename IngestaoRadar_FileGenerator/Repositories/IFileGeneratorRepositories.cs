using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IFileGeneratorRepositories
    {
        public List<Radar> SelectAllMongo();
        public List<Radar> SelectAllSql();
    }
}
