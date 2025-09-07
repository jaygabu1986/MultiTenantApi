using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using MultiTenantApi.Data;
using MultiTenantApi.Models;

namespace MultiTenantApi.Services
{
    public class TenantProvider : ITenantProvider
    {
        private readonly MasterDbContext _masterDb;
        private readonly IMemoryCache _cache;
        private readonly ILogger<TenantProvider> _logger;
        private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(15);

        public TenantProvider(MasterDbContext masterDb, IMemoryCache cache, ILogger<TenantProvider> logger)
        {
            _masterDb = masterDb;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Tenant?> GetTenantByHostAsync(string host)
        {
            if (string.IsNullOrWhiteSpace(host)) return null;

            if (_cache.TryGetValue(host, out Tenant tenant))
                return tenant;

            tenant = await _masterDb.Tenants
                        .AsNoTracking()
                        .FirstOrDefaultAsync(t => t.DomainName == host && t.IsActive);

            if (tenant != null)
                _cache.Set(host, tenant, CacheDuration);

            return tenant;
        }
    }
}
