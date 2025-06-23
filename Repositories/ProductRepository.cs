using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        PettsStoreContext _pettsStoreContext;
        public ProductRepository(PettsStoreContext pettsStoreContext)
        {
            _pettsStoreContext = pettsStoreContext;
        }
        public async Task<Product> GetProductById(int id) // פונקציה עם אות קטנה - לשנות ל-GetProductById
        {
            // אפשר לכתוב בקיצור: return await _pettsStoreContext.Products.FirstOrDefaultAsync(product => product.Id == id);
            // LAMBDA: return await _pettsStoreContext.Products.FirstOrDefaultAsync(product => product.Id == id);
            Product product = await _pettsStoreContext.Products.FirstOrDefaultAsync(product => product.Id == id); // למחוק שגיאת כתיב produt
            return product;
        }
        public async Task<List<Product>> GetAllProducts(string? desc, int? minPrice, int? maxPrice, int?[] categoriesIds ) // פונקציה עם אות קטנה - לשנות ל-GetAllProducts
        {
            // אפשר לכתוב בקיצור: return await _pettsStoreContext.Products.Where(...).OrderBy(...).ToListAsync();
            // LAMBDA: return await _pettsStoreContext.Products.Where(...).OrderBy(...).ToListAsync();
            var query = _pettsStoreContext.Products.Where(product => (desc == null ? (true) : (product.Description.Contains(desc)))
            && ((minPrice == null) ? (true) : (product.Price >= minPrice))
            && ((maxPrice == null) ? (true) : (product.Price <= maxPrice))
            && ((categoriesIds.Length == 0) ? true : categoriesIds.Contains(product.CategoryId)))
            .OrderBy(product=>product.Price);
            List<Product> products = await query.ToListAsync();
            return products;
        }
    }
}
