using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using DTOs;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace pettsStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IProductServise productService;
        public ProductsController(IProductServise productService)
        {
            this.productService = productService;
        }
        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> Get([FromQuery]string? desc, [FromQuery] int? minPrice, [FromQuery] int? maxPrice, [FromQuery] int?[] categoriesIds)
        {
            return await productService.getAllProducts(desc, minPrice, maxPrice, categoriesIds);
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            return await productService.getProductById(id);
        }

    }
}
