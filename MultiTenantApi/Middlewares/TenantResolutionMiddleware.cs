using MultiTenantApi.Services;

namespace MultiTenantApi.Middlewares
{
    public class TenantResolutionMiddleware
    {
        private readonly RequestDelegate _next;
        public TenantResolutionMiddleware(RequestDelegate next) { _next = next; }

        public async Task InvokeAsync(HttpContext context, ITenantProvider tenantProvider)
        {
            var host = context.Request.Host.Host; // e.g. tenant1.local
            var tenant = await tenantProvider.GetTenantByHostAsync(host);

            if (tenant == null)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Tenant not found.");
                return;
            }

            context.Items["Tenant"] = tenant;
            await _next(context);
        }
    }

}
