using Xunit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Repositories;
using Entities;
using Bakery;
using System.Collections.Generic;
using System.Linq;

public class ProductsDataIntegrationTests :IClassFixture<DatabaseFixture>
{
    private readonly BakeryDBContext _context;
    private readonly ProductsData _productsData;
    public ProductsDataIntegrationTests(DatabaseFixture fixture)
    {
        _context = fixture.Context;
        _productsData = new ProductsData(_context);
    }


    [Fact]
    public async Task GetProducts_ReturnsProductsWithCategories()
    {
      

            var category = new Catgory {  CatgoryName = "Cakes" };
            var product = new Product
            {
                ProductName = "Chocolate Cake",
                Price = 25,
                CategoryId = 1,
                Category = category
            };

            _context.Catgories.Add(category);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
   
            var productsData = new ProductsData(_context);

        // Act
        var result = await productsData.getProducts(
            desc: "cake",                      
            minPrice: 20,                      
            maxPrice: 100,                     
            categoryIds: new int?[] { 1, 3 } 
            );
        // Assert
        Assert.Single(result);
            Assert.Equal("Chocolate Cake", result.First().ProductName);
            Assert.NotNull(result.First().Category);
            Assert.Equal("Cakes", result.First().Category.CatgoryName);
        
    }
}