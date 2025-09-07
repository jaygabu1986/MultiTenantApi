using Microsoft.EntityFrameworkCore;
using MultiTenantApi.Models;

namespace MultiTenantApi.Data
{
    public class MasterDbContext : DbContext
    {
        public MasterDbContext(DbContextOptions<MasterDbContext> options) : base(options) { }
        public DbSet<Tenant> Tenants { get; set; } = null!;
    }
}
