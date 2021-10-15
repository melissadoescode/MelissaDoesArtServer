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
    public class ProductRepository : DbConnectionDevRepository, IProductRepository
    {
        public ProductRepository(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
        {

        }
        public async Task<int> Create(Product product)
        {
            return await DbConnection.ExecuteAsync("CreateProduct",
                new
                {
                    ProductName = product.ProductName,
                    ProductImage = product.ProductImage,
                    ProductDescription = product.ProductDescription,
                    ProductPrice = product.ProductPrice,
                }, commandType: CommandType.StoredProcedure);
        }

        public async Task<List<Product>> GetAll()
        {
            var get = await DbConnection.QueryAsync<Product>("GetAllProducts", commandType: CommandType.StoredProcedure);
            return get.ToList();
        }

        public async Task<Product> GetById(int productId)
        {
            var getById = await DbConnection.QueryAsync<Product>("GetProductById",
                new
                {
                    ProductId = productId
                }, commandType: CommandType.StoredProcedure);
            return getById.FirstOrDefault();
        }

        public async Task<int> Update(Product product)
        {
            return await DbConnection.ExecuteAsync("UpdateProduct",
               new
               {
                   ProductId = product.ProductId,
                   ProductName = product.ProductName,
                   ProductImage = product.ProductImage,
                   ProductDescription = product.ProductDescription,
                   ProductPrice = product.ProductPrice,
               }, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> Delete(int productId)
        {
            return await DbConnection.ExecuteAsync("DeleteProduct",
                new
                {
                    ProductId = productId
                }, commandType: CommandType.StoredProcedure);
        }
    }
}
