using AutoMapper;
using DTOs;
using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductsServices : IProductsServices
    {
        private readonly IProductsData productsData;
        private readonly IMapper autoMapping;

        public ProductsServices(IProductsData _productsData, IMapper _autoMapping)
        {
            autoMapping = _autoMapping;
            productsData = _productsData;
        }
        public async Task<List<ProductDto>> GetProducts(string? desc, int? minPrice, int? maxPrice, int?[]? categoryIds) // Changed to PascalCase: GetProducts
        {
            List<Product>  products = await productsData.getProducts(desc,minPrice, maxPrice,categoryIds); // Suggestion: getProducts => GetProducts (PascalCase)
            return autoMapping.Map<List<Product>, List<ProductDto>>(products);
        }
    }
}
