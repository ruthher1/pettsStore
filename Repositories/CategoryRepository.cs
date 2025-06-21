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

        public async Task<Category> getCategoryById(int id)
        {
            Category category = await _pettsStoreContext.Categories.FirstOrDefaultAsync(category => category.Id == id);
            return category;
        }

        public async Task<List<Category>> getAllCategories()
        {
            return await _pettsStoreContext.Categories.ToListAsync();
        }
    }
}
