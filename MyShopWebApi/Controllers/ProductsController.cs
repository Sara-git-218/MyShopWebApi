using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShopWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        // GET: api/<ProductsController>
        [HttpGet]
      
        public async Task<List<ProductDTO>> Get(
                [FromQuery] string? desc,
                [FromQuery] int? minPrice,
                [FromQuery] int? maxPrice,
                [FromQuery] int?[] categoryIds)
        {

            return await _productService.GetProducts(desc, minPrice, maxPrice, categoryIds);
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

      
    }
}
