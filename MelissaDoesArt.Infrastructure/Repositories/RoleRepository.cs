using Dapper;
using MelissaDoesArt.Infrastructure.Interfaces;
using MelissaDoesArt.Infrastructure.Interfaces.Dapper;
using MelissaDoesArt.Infrastructure.Models;
using MelissaDoesArt.Infrastructure.Repositories.DbConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelissaDoesArt.Infrastructure.Repositories
{
    public class RoleRepository : DbConnectionDevRepository, IRoleRepository
    {
        public RoleRepository(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
        {

        }
        public async Task<int> Create(Role role)
        {
            return await DbConnection.ExecuteAsync("CreateRole",
                new
                {
                    RoleType = role.RoleType,
                }, commandType: CommandType.StoredProcedure);
        }

        public async Task<List<Role>> GetAll()
        {
            var get = await DbConnection.QueryAsync<Role>("GetAllRoles", commandType: CommandType.StoredProcedure);
            return get.ToList();
        }

        public async Task<Role> GetById(int roleId)
        {
            var getById = await DbConnection.QueryAsync<Role>("GetRoleById",
                new
                {
                    RoleId = roleId
                }, commandType: CommandType.StoredProcedure);
            return getById.FirstOrDefault();
        }

        public async Task<int> Update(Role role)
        {
            return await DbConnection.ExecuteAsync("UpdateRole",
               new
               {
                   RoleId = role.RoleId,
                   RoleType = role.RoleType
               }, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> Delete(int roleId)
        {
            return await DbConnection.ExecuteAsync("DeleteRole",
                new
                {
                    RoleId = roleId
                }, commandType: CommandType.StoredProcedure);
        }
    }
}
