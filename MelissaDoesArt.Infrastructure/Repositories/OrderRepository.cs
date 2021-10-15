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
    public class OrderRepository : DbConnectionDevRepository, IOrderRepository
    {
        public OrderRepository(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
        {

        }
        public async Task<int> Create(Order order)
        {
            return await DbConnection.ExecuteAsync("CreateOrder",
                new
                {
                    PaymentId = order.PaymentId,
                    CheckoutId = order.CheckoutId,
                    CartId = order.CartId,
                    ProductId = order.ProductId,
                    OrderStatus =order.OrderStatus,
                    OrderDate = DateTime.Now
                }, commandType: CommandType.StoredProcedure);
        }

        public async Task<List<Order>> GetAll()
        {
            var get = await DbConnection.QueryAsync<Order>("GetAllOrders", commandType: CommandType.StoredProcedure);
            return get.ToList();
        }

        public async Task<Order> GetById(int orderId)
        {
            var getById = await DbConnection.QueryAsync<Order>("GetOrderById",
                new
                {
                    OrderId = orderId
                }, commandType: CommandType.StoredProcedure);
            return getById.FirstOrDefault();
        }

        public async Task<int> Update(Order order)
        {
            return await DbConnection.ExecuteAsync("UpdateOrder",
               new
               {
                   OrderId = order.OrderId,
                   PaymentId = order.PaymentId,
                   CheckoutId = order.CheckoutId,
                   CartId = order.CartId,
                   ProductId = order.ProductId,
                   OrderStatus = order.OrderStatus,
                   OrderDate = DateTime.Now
               }, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> Delete(int orderId)
        {
            return await DbConnection.ExecuteAsync("DeleteOrder",
                new
                {
                    OrderId = orderId
                }, commandType: CommandType.StoredProcedure);
        }
    }
}
