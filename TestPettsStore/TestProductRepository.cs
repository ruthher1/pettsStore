using Entities;
using Moq;
using Repositories;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPettsStore
{
   public class TestProductRepository
    {
        [Fact]
        public async Task GetAllProducts_ReturnAllProducts()
        {
            var product1 = new Product { Id = 1, CategoryId =1 ,Price=20,Name="dog",Description="nice dog" };
            var product2 = new Product { Id = 1, CategoryId = 1, Price = 20, Name = "dog", Description = "nice dog" };

            var mockContext = new Mock<PettsStoreContext>();
            var products = new List<Product>() { product1, product2 };
            mockContext.Setup(x => x.Products).ReturnsDbSet(products);
            var productRepository = new ProductRepository(mockContext.Object);

            var result = await productRepository.getAllProducts(null,null, null, []);

            Assert.Equal(2, result.Count());
            Assert.Equal(product1, products.First());

        }

        [Fact]
        public async Task GetProduct_ReturnProduct()
        {
            var product1 = new Product { Id = 1, CategoryId = 1, Price = 20, Name = "dog", Description = "nice dog" };
            var product2 = new Product { Id = 2, CategoryId = 1, Price = 20, Name = "dog", Description = "nice dog" };

            var mockContext = new Mock<PettsStoreContext>();
            var products = new List<Product>() { product1, product2 };
            mockContext.Setup(x => x.Products).ReturnsDbSet(products);
            var productRepository = new ProductRepository(mockContext.Object);


            var result = await productRepository.getProductById(product2.Id);

            Assert.Equal(product2, result);

        }
    }
}
