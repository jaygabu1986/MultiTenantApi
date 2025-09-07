using MultiTenantApi.Data;

namespace MultiTenantApi.Factory
{
    public interface ITenantDbContextFactory
    {
        TenantDbContext CreateDbContext(string connectionString);
    }

}
