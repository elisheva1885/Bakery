using DTOs;
using Entities;

namespace Services
{
    public interface IProductsServices
    {
        Task<List<ProductDto>> getProducts(string? desc, int? minPrice, int? maxPrice, int?[] categoryIds);
    }
}