using Microsoft.EntityFrameworkCore;
using MultiTenantApi.Models;

namespace MultiTenantApi.Data
{
    public class TenantDbContext : DbContext
    {
        public TenantDbContext(DbContextOptions<TenantDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; } = null!;
    }
}
