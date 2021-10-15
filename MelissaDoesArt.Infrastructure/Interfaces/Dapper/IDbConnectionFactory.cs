using MelissaDoesArt.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelissaDoesArt.Infrastructure.Interfaces.Dapper
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateDbConnection(DbConnectionName connectionName);
    }
}
