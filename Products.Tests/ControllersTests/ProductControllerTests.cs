using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Moq;
using Products.Api.Controllers;
using Products.Data.Models;
using Products.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Products.Tests.ServicesTests
{
    public class MockProductService : Mock<IProductService>
    {
        public void SetMockRetrieveProducts(ICollection<Product> prodcts, string prodctName = null)
        {
            Setup(p => p.RetrieveProducts(prodctName)).ReturnsAsync(() => { return prodcts; });
        }

        public void SetMockAddProduct(Product product)
        {
            Setup(p => p.AddProduct(product)).ReturnsAsync(() => { return product; });
        }
    }

    public class MockProductOptionService : Mock<IProductOptionService>{}

    public class ProductControllerTests
    {
        [Fact]
        public async Task RetrieveProductsTest()
        {
            //Arrange
            var prodct1 = new Product()
            {
                Id = Guid.NewGuid(),
                Name = "Ipad",
                Description = "Good",
                Price = 10,
                DeliveryPrice = 20,
            };
            var prodct2 = new Product()
            {
                Id = Guid.NewGuid(),
                Name = "Kindle",
                Description = "Good",
                Price = 10,
                DeliveryPrice = 20,
            };
            var products = new ProductCollection();
            products.Items.Add(prodct1);
            products.Items.Add(prodct2);

            //Act
            var mockProductService = new MockProductService();
            mockProductService.SetMockRetrieveProducts(products.Items);
            var mockProductOptionService = new MockProductOptionService();
            var productController = new ProductsController(mockProductService.Object, mockProductOptionService.Object);
            var d = await productController.Get();

            //Assert
            Assert.Equal(2, d.Value.Items.Count);
        }
    }
}
