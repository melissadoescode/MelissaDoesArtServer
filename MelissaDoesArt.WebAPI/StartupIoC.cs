using MelissaDoesArt.Infrastructure.Factories;
using MelissaDoesArt.Infrastructure.Interfaces;
using MelissaDoesArt.Infrastructure.Interfaces.Dapper;
using MelissaDoesArt.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace MelissaDoesArt.WebAPI
{
    public class StartupIoC
    {
        public static void Setup(IServiceCollection services)
        {
            SetupScoped(services);
            SetupTransient(services);
            SetupSingleton(services);

        }

        private static void SetupScoped(IServiceCollection services)
        {
            // Scoped - Creates a new instance per web request 
            services
                //Database
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IRoleRepository, RoleRepository>()
                .AddScoped<IProductRepository, ProductRepository>()
                .AddScoped<ICartRepository, CartRepository>()
                .AddScoped<IOrderRepository, OrderRepository>();
        }


        private static void SetupTransient(IServiceCollection services)
        {
            // Transient - Always a new instance
        }
        private static void SetupSingleton(IServiceCollection services)
        {
            // Singleton - Always the same instance 
            services
                .AddTransient<IDbConnectionFactory, DbConnectionFactory>()
                .AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
