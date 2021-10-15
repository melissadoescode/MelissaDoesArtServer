using MelissaDoesArt.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelissaDoesArt.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IUserRepository userRepository, IRoleRepository roleRepository, IProductRepository productRepository, ICartRepository cartRepository, IOrderRepository orderRepository)
        {
            Users = userRepository;
            Roles = roleRepository;
            Products = productRepository;
            Carts = cartRepository;
            Orders = orderRepository;
        }
        public IUserRepository Users { get; }
        public IRoleRepository Roles { get; }
        public IProductRepository Products { get; }
        public ICartRepository Carts { get; }
        public IOrderRepository Orders { get; }
    }
}
