using AutoMapper;
using DTOs;
using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
       public class ProductServise : IProductServise
      {
        IProductRepository productRepository;// _productRepository
        private readonly IMapper _mapper;

        public ProductServise(IProductRepository productRepository,IMapper mapper)
        {
            this.productRepository = productRepository;// _productRepository = productRepository;
            _mapper = mapper;

        }
        public async Task<ProductDTO> getProductById(int id)//GetProductById
        {
            return _mapper.Map<Product,ProductDTO > (await productRepository.getProductById(id));
        }
        public async Task<List<ProductDTO>> getAllProducts(string? desc, int? minPrice, int? maxPrice, int?[] categoriesIds)//GetAllProducts
        {
            return _mapper.Map< List < Product>, List<ProductDTO>> (await productRepository.getAllProducts(desc, minPrice, maxPrice, categoriesIds));
        }


    }
}
