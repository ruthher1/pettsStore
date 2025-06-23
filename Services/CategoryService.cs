using Entities;
using Repositories;
using DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        ICategoryRepository categoryRepository;//_categoryRepository
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;//_categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<CategoryDTO> getCategoryById(int id)// GetCategoryById
        {
            return _mapper.Map<Category, CategoryDTO>(await categoryRepository.getCategoryById(id));
        }
        public async Task<List<CategoryDTO>> getAllCategories()// GetAllCategories
        {
            return _mapper.Map<List<Category>, List<CategoryDTO>>(await categoryRepository.getAllCategories());
        }
    }
}
