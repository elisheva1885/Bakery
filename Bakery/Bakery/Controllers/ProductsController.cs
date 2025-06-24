using DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
// delete unused code
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bakery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsServices productsServices;//_productsServices;
        public ProductsController(IProductsServices _productsServices)
        {
            productsServices = _productsServices;
        }
        // GET: api/<ProductsController>//
        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetProducts(string? desc, int? minPrice, int? maxPrice, int?[] categoryIds)
        {
            return await productsServices.GetProducts(desc, minPrice, maxPrice, categoryIds); // Suggestion: getProducts => GetProducts (PascalCase)
        }

    
    }
}
