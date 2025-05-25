using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShopWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatgoryController : ControllerBase
    {
        private readonly ICatgoryService _catgoryService;
        public CatgoryController(ICatgoryService catgoryService)
        {
            _catgoryService = catgoryService;
        }
        // GET: api/<CatgoryController>
        [HttpGet]
        public async Task<List<CategoryDTO>> Get()
        {
            return await _catgoryService.GetCatgories();
        }

    }
}
