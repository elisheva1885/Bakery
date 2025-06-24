using Repositories;
using Entities;
using Bakery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DTOs;

namespace Services
{
    public class CategoriesServices : ICategoriesServices
    {
    private readonly ICategoriesData categoriesData;//_categoriesData;

        public CategoriesServices(ICategoriesData _categoriesData, IMapper _autoMapping)
        {
            autoMapping = _autoMapping;
            categoriesData = _categoriesData;
        }
        public async Task<List<CategoryDto>> GetCategories() // Changed to PascalCase: GetCategories
        {
             List<Catgory> categories = await categoriesData.getCatgories(); // Suggestion: getCatgories => GetCatgories (PascalCase)
            return autoMapping.Map<List<CategoryDto>>(categories);
        }
    }
}
