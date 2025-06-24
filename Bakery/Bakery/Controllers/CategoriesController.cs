using DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Services;
// delete unused code
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bakery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesServices categoriesServices;//_categoriesServices;


        public CategoriesController(ICategoriesServices _categoriesServices)
        {
            categoriesServices = _categoriesServices;
        }
        // GET: api/<CategoriesController>//
        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> GetCategories()
        {
            return await categoriesServices.getCategories(); // Suggestion: getCategories => GetCategories (PascalCase)
        }



       
    }
}
