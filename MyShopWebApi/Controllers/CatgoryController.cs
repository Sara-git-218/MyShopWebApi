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
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
        public async Task<List<Catgory>> Get()
        {
            return await _catgoryService.GetCatgories();
        }

        // GET api/<CatgoryController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<CatgoryController>
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        // PUT api/<CatgoryController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/<CatgoryController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
