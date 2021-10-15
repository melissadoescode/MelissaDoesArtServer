using Microsoft.EntityFrameworkCore;

namespace MelissaDoesArt.WebAPI.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
