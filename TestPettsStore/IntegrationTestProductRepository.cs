using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPettsStore
{
     public class IntegrationTestProductRepository : IClassFixture<DatabaseFixture>
    {

        private readonly PettsStoreContext _dbContext;
        private readonly ProductRepository _productRepository;

        public IntegrationTestProductRepository(DatabaseFixture databaseFixture)
        {
            this._dbContext = databaseFixture.Context;
            this._productRepository = new ProductRepository(this._dbContext);
        }


        [Fact]
        public async Task GetProductById_ExistingProduct_ReturnsProduct()
        {
            await _dbContext.Categories.AddRangeAsync(new List<Category>
     {
         new Category { CategoryName = "Category1" },
         new Category { CategoryName = "Category2" }
     });
            await _dbContext.SaveChangesAsync();
            var product = new Product {Name = "Test Product", Price = 10.0, Description = "Test Description", CategoryId = 1 };
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            var result = await _productRepository.getProductById(1);

            Assert.NotNull(result);
            Assert.Equal(product.Name, result.Name);
        }

        [Fact]
        public async Task GetAllProducts_WithFilters_ReturnsFilteredProducts()
        {
            await _dbContext.Categories.AddRangeAsync(new List<Category>
     {
         new Category { CategoryName = "Category1" },
         new Category { CategoryName = "Category2" }
     });
            await _dbContext.SaveChangesAsync();

            var product1 = new Product {  Name = "Test Product 1", Price = 10.0, Description = "A product", CategoryId = 1 };
            var product2 = new Product {  Name = "Test Product 2", Price = 20.0, Description = "Another product", CategoryId = 2 };
            var product3 = new Product {  Name = "Test Product 3", Price = 30.0, Description = "A product in category 1", CategoryId = 1 };

            await _dbContext.Products.AddRangeAsync(product1, product2, product3);
            await _dbContext.SaveChangesAsync();

            var result = await _productRepository.getAllProducts("product", 15, null, new int?[] { 1 });

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(product3.Name, result[0].Name);
        }

        [Fact]
        public async Task GetAllProducts_NoProducts_ReturnsEmptyList()
        {
            var result = await _productRepository.getAllProducts(null, null, null, new int?[] { });

            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}

