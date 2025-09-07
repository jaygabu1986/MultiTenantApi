using MultiTenantApi.Models;

namespace MultiTenantApi.Services
{
    public interface ITenantProvider
    {
        Task<Tenant?> GetTenantByHostAsync(string host);
    }
}
