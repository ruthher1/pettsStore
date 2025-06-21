using DTOs;
using Entities;

namespace Services
{
    public interface IProductServise
    {
        Task<List<ProductDTO>> getAllProducts(string? desc, int? minPrice, int? maxPrice, int?[] categoriesIds);
        Task<ProductDTO> getProductById(int id);
    }
}