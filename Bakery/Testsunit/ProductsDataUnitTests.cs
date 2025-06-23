using Entities;
using Moq;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Moq.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bakery; // או כל namespace שבו Product מוגדר
using Repositories; //
namespace Testsunit;

public class ProductsDataUnitTests
{
    [Fact]
    public async Task getProducts_ReturnsAllProducts()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, ProductName = "Bread" },
            new Product { Id = 2, ProductName = "Cake" }
        };

        var mockContext = new Mock<BakeryDBContext>(new Microsoft.EntityFrameworkCore.DbContextOptions<BakeryDBContext>());
        mockContext.Setup(x => x.Products).ReturnsDbSet(products);

        var repo = new ProductsData(mockContext.Object);

        // Act
        var result = await repo.getProducts(
            desc: "cake",
            minPrice: 20,
            maxPrice: 100,
            categoryIds: new int?[] { 1, 3 }
            );
        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("Bread", result[0].ProductName);
        Assert.Equal("Cake", result[1].ProductName);
    }
}