using MelissaDoesArt.Infrastructure.Enum;
using MelissaDoesArt.Infrastructure.Interfaces.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelissaDoesArt.Infrastructure.Factories
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly IDictionary<DbConnectionName, string> connectionString;

        public DbConnectionFactory(IDictionary<DbConnectionName, string> connectionString)
        {
            this.connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public IDbConnection CreateDbConnection(DbConnectionName connectionName)
        {
            string conn = null;
            if (connectionString.TryGetValue(connectionName, out conn))
            {
                return new SqlConnection(conn);
            }
            throw new ArgumentNullException();
        }
    }
}
