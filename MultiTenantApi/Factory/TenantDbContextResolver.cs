using MultiTenantApi.Data;
using MultiTenantApi.Models;

namespace MultiTenantApi.Factory
{
    public class TenantDbContextResolver : ITenantDbContextResolver
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITenantDbContextFactory _factory;

        public TenantDbContextResolver(IHttpContextAccessor httpContextAccessor, ITenantDbContextFactory factory)
        {
            _httpContextAccessor = httpContextAccessor;
            _factory = factory;
        }

        public TenantDbContext GetTenantDbContext()
        {
            var tenant = _httpContextAccessor.HttpContext?.Items["Tenant"] as Tenant;
            if (tenant == null) throw new InvalidOperationException("Tenant not resolved.");
            return _factory.CreateDbContext(tenant.ConnectionString);
        }
    }

}
