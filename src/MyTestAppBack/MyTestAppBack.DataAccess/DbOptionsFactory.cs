using Microsoft.EntityFrameworkCore;
using MyTestAppBack.DataAccess.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTestAppBack.DataAccess
{
    public static class DbOptionsFactory
    {
        private static readonly DbContextOptionsBuilder _dbOptions = new();
        private static readonly Dictionary<DbType, Func<string, DbContextOptions>> _dbOptionsContainer = new()
        {
            { DbType.Sqlite, (connectionString) => _dbOptions.UseSqlite(connectionString).Options },
            { DbType.SqlServer, (connectionString) => _dbOptions.UseSqlServer(connectionString).Options },
        };

        public static DbContextOptions GetOptionsViaDb(DbType dbType, string connectionString)
        {
            if (!_dbOptionsContainer.ContainsKey(dbType)) throw new KeyNotFoundException("Не задана конфигурация для данной БД");

            var options = _dbOptionsContainer[dbType](connectionString);

            return options;
        }
    }
}
