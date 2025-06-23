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
        public async Task<List<ProductDto>> getProducts(string? desc, int? minPrice, int? maxPrice, int?[]? categoryIds)
        {
            List<Product>  products = await productsData.getProducts(desc,minPrice, maxPrice,categoryIds);
            List<ProductDto> productsDto = autoMapping.Map<List<Product>, List<ProductDto>>(products);
            return productsDto;
        }
    }
}
