using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ProductsData : IProductsData
    {
        BakeryDBContext dBContext;//_dBContext;
        public ProductsData(BakeryDBContext usersDBContext)
        {
            dBContext = usersDBContext;
        }

        public async Task<List<Product>> GetProducts(string? desc,int? minPrice, int? maxPrice, int?[] categoryIds ) // Change to PascalCase: GetProducts
        {
            var getProductsQuery = dBContext.Products.Include(p => p.Category).Where(product =>
                (desc == null ? (true) : (product.ProductDescription.Contains(desc)))
                && ((minPrice == null) ? (true) : (product.Price >= minPrice))
                && ((maxPrice == null) ? (true) : (product.Price <= maxPrice))
                && ((categoryIds.Length == 0) ? (true) : (categoryIds.Contains(product.CategoryId))))
                    .OrderBy(product => product.Price);
            return await getProductsQuery.ToListAsync();
        }
    }
}
