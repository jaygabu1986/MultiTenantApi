using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiTenantApi.Factory;

namespace MultiTenantApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ITenantDbContextResolver _dbResolver;

        public ProductsController(ITenantDbContextResolver dbResolver)
        {
            _dbResolver = dbResolver;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            using var db = _dbResolver.GetTenantDbContext();
            var products = await db.Products.AsNoTracking().ToListAsync();
            return Ok(products);
        }
    }

}
