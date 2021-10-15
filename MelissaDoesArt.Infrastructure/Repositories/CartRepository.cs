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
    public class CartRepository : DbConnectionDevRepository, ICartRepository
    {
        public CartRepository(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
        {

        }
        public async Task<int> Create(Cart cart)
        {
            return await DbConnection.ExecuteAsync("CreateCart",
                new
                {
                    ProductId = cart.CartId,
                    Quantity = cart.Quantity,
                    TotalCost = cart.TotalCost,
                }, commandType: CommandType.StoredProcedure);
        }

        public async Task<List<Cart>> GetAll()
        {
            var get = await DbConnection.QueryAsync<Cart>("GetAllCarts", commandType: CommandType.StoredProcedure);
            return get.ToList();
        }

        public async Task<Cart> GetById(int cartId)
        {
            var getById = await DbConnection.QueryAsync<Cart>("GetCartById",
                new
                {
                    CartId = cartId
                }, commandType: CommandType.StoredProcedure);
            return getById.FirstOrDefault();
        }

        public async Task<int> Update(Cart cart)
        {
            return await DbConnection.ExecuteAsync("UpdateCart",
               new
               {
                   ProductId = cart.CartId,
                   Quantity = cart.Quantity,
                   TotalCost = cart.TotalCost
               }, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> Delete(int cartId)
        {
            return await DbConnection.ExecuteAsync("DeleteCart",
                new
                {
                    CartId = cartId
                }, commandType: CommandType.StoredProcedure);
        }
    }
}
