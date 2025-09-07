using MultiTenantApi.Data;

namespace MultiTenantApi.Factory
{
    public interface ITenantDbContextResolver
    {
        TenantDbContext GetTenantDbContext(); // caller must dispose
    }

}
