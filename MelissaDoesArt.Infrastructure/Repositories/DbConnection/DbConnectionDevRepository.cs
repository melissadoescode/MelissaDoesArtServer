using MelissaDoesArt.Infrastructure.Enum;
using MelissaDoesArt.Infrastructure.Interfaces.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelissaDoesArt.Infrastructure.Repositories.DbConnection
{
    public class DbConnectionDevRepository
    {
        public IDbConnection DbConnection { get; private set; }

        public DbConnectionDevRepository(IDbConnectionFactory dbConnectionFactory)
        {
            this.DbConnection = dbConnectionFactory.CreateDbConnection(DbConnectionName.MelissaDoesArtDev);
        }
    }
}
