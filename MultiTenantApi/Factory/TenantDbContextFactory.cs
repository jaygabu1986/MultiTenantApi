namespace MultiTenantApi.Factory
{
    using Microsoft.EntityFrameworkCore;
    using MultiTenantApi.Data;

    public class TenantDbContextFactory : ITenantDbContextFactory
    {
        public TenantDbContext CreateDbContext(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TenantDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new TenantDbContext(optionsBuilder.Options);
        }
    }

}
