using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        PettsStoreContext _pettsStoreContext;
        public CategoryRepository(PettsStoreContext pettsStoreContext)
        {
            _pettsStoreContext = pettsStoreContext;
        }
        public async Task<Category> GetCategoryById(int id) // פונקציה עם אות קטנה - לשנות ל-GetCategoryById
        {
            // אפשר לכתוב בקיצור: return await _pettsStoreContext.Categories.FirstOrDefaultAsync(category => category.Id == id);
            // LAMBDA: return await _pettsStoreContext.Categories.FirstOrDefaultAsync(category => category.Id == id);
            Category category = await _pettsStoreContext.Categories.FirstOrDefaultAsync(category => category.Id == id);
            return category;
        }
        public async Task<List<Category>> GetAllCategories() // פונקציה עם אות קטנה - לשנות ל-GetAllCategories
        {
            // אפשר לכתוב בקיצור: return await _pettsStoreContext.Categories.ToListAsync();
            // LAMBDA: return await _pettsStoreContext.Categories.ToListAsync();
            return await _pettsStoreContext.Categories.ToListAsync();
        }
    }
}
