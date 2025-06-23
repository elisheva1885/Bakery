using Entities;

namespace Repositories
{
    public interface IProductsData
    {
        Task<List<Product>> getProducts(string? desc, int? minPrice, int? maxPrice, int?[] categoryIds);
    }
}