using Entities;
using Repositories;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPettsStore
{
    public class TestCategoryRepository
    {
        [Fact]
        public async Task GetAllCategories_ReturnAllCategories()
        {
            var category1 = new Category { Id = 1, CategoryName = "Food" };
            var category2 = new Category { Id = 2, CategoryName = "Games" };

            var mockContext = new Mock<PettsStoreContext>();
            var categories = new List<Category>() { category1, category2 };
            mockContext.Setup(x => x.Categories).ReturnsDbSet(categories);
            var categoryRepository = new CategoryRepository(mockContext.Object);

            var result = await categoryRepository.getAllCategories();

            Assert.Equal(2, result.Count());

        }

        [Fact]
        public async Task GetCategory_ReturnCategory()
        {
            var category1 = new Category { Id = 1, CategoryName = "Food" };
            var category2 = new Category { Id = 2, CategoryName = "Games" };

            var mockContext = new Mock<PettsStoreContext>();
            var categories = new List<Category>() { category1, category2 };
            mockContext.Setup(x => x.Categories).ReturnsDbSet(categories);
            var categoryRepository = new CategoryRepository(mockContext.Object);

            var result = await categoryRepository.getCategoryById(2);

            Assert.Equal(category2, result);
        }
    }
}
