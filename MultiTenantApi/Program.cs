using Microsoft.EntityFrameworkCore;
using MultiTenantApi.Data;
using MultiTenantApi.Factory;
using MultiTenantApi.Middlewares;
using MultiTenantApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Configuration
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();
builder.Services.AddControllers();

// Master DB (EF Core) - for tenants metadata
builder.Services.AddDbContext<MasterDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MasterDb")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Tenant tooling
builder.Services.AddScoped<ITenantProvider, TenantProvider>();
builder.Services.AddScoped<ITenantDbContextFactory, TenantDbContextFactory>();
builder.Services.AddScoped<ITenantDbContextResolver, TenantDbContextResolver>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Use tenant middleware early (before MVC)
app.UseMiddleware<TenantResolutionMiddleware>();
app.MapControllers();

app.Run();
