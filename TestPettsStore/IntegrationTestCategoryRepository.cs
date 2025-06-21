using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPettsStore
{
    public class IntegrationTestCategoryRepository : IClassFixture<DatabaseFixture>
    {
        private readonly PettsStoreContext _dbContext;
        private readonly CategoryRepository _categoryRepository;

        public IntegrationTestCategoryRepository(DatabaseFixture databaseFixture)
        {
            this._dbContext = databaseFixture.Context;
            this._categoryRepository = new CategoryRepository(this._dbContext);
        }

        [Fact]
        public async Task GetAllCategories_NoCategories_ReturnsEmptyList()
        {

            var result = await _categoryRepository.getAllCategories();

            Assert.NotNull(result);
            Assert.Empty(result); // Expecting an empty list
        }

        [Fact]
        public async Task GetCategoryById_ValidId_ReturnsCategory()
        {
            var category = new Category { CategoryName = "TestCategory" };
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            var result = await _categoryRepository.getCategoryById(category.Id);

            Assert.NotNull(result);
            Assert.Equal(category.CategoryName, result.CategoryName);
        }

        [Fact]
        public async Task GetCategoryById_NonExistingId_ReturnsNull()
        {
            var result = await _categoryRepository.getCategoryById(999); // Assuming this ID does not exist

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllCategories_ReturnsAllCategories()
        {
            await _dbContext.Categories.AddRangeAsync(new List<Category>
     {
         new Category { CategoryName = "Category1" },
         new Category { CategoryName = "Category2" }
     });
            await _dbContext.SaveChangesAsync();

            var result = await _categoryRepository.getAllCategories();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count); // Expecting two categories
        }
    }
}