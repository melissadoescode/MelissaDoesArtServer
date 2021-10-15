using Dapper;
using MelissaDoesArt.Infrastructure.Interfaces;
using MelissaDoesArt.Infrastructure.Interfaces.Dapper;
using MelissaDoesArt.Infrastructure.Models;
using MelissaDoesArt.Infrastructure.Repositories.DbConnection;
using MelissaDoesArt.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;


namespace MelissaDoesArt.Infrastructure.Repositories
{
    public class UserRepository : DbConnectionDevRepository, IUserRepository
    {
        public UserRepository(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory) 
        {

        }
        public async Task<int> Create(User user)
        {
            return await DbConnection.ExecuteAsync("CreateUser",
                new
                {
                    RoleId = user.RoleId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
                    Verified = EmailValidationService.IsEmailValid(user.Email),
                    CreatedDate = DateTime.Now
                }, commandType: CommandType.StoredProcedure);
        }

        public async Task<List<User>> GetAll()
        {
            var get = await DbConnection.QueryAsync<User>("GetAllUsers", commandType: CommandType.StoredProcedure);
            return get.ToList();
        }

        public async Task<User> GetUser(string username, string password)
        {
            var users = await DbConnection.QueryAsync<User>("GetAllUsers", commandType: CommandType.StoredProcedure);
            return users.Where(x => x.Email.ToLower() == username.ToLower() && x.Password== password).FirstOrDefault();
        }

        public async Task<User> GetById(int userId)
        {
            var getById = await DbConnection.QueryAsync<User>("GetUserById",
                new
                {
                    UserId = userId
                }, commandType: CommandType.StoredProcedure);
            return getById.FirstOrDefault();
        }

        public async Task<int> Update(User user)
        {
            return await DbConnection.ExecuteAsync("UpdateUser",
               new
               {
                   UserId = user.UserId,
                   RoleId = user.RoleId,
                   FirstName = user.FirstName,
                   LastName = user.LastName,
                   Email = user.Email,
                   Password = BC.HashPassword(user.Password).ToString(),
                   Verified = EmailValidationService.IsEmailValid(user.Email),
               }, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> Delete(int userId)
        {
            return await DbConnection.ExecuteAsync("DeleteUser",
                new
                {
                    UserId = userId
                }, commandType: CommandType.StoredProcedure);
        }
    }
}
