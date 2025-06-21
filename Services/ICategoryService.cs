using DTOs;
using Entities;

namespace Services
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> getAllCategories();
        Task<CategoryDTO> getCategoryById(int id);
    }
}