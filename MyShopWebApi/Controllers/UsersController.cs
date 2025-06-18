using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Services;
using DTO;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShopWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;
        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService= userService;
            _logger= logger;
        }
      
        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsersController>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        [HttpPost("register")]
        public  async Task<ActionResult<UserDTO>> Register([FromBody] UserRegisterDTO user)
        {

            UserDTO newusewr = await _userService.Register(user);
            if (newusewr != null)
            {
                return Ok(newusewr);
            }
            return StatusCode(400,"try Again");
          


        }

        [HttpPost("login")]
        public async  Task<ActionResult<UserDTO>> Login([FromBody] UserLoginDTO user)
        {


            UserDTO u =  await _userService.Login(user);
            if (u!=null)
            {
                _logger.LogInformation("user " + u.UserName + "logged in successfully at " + DateTime.UtcNow);
                return Ok(u);
            }
            return StatusCode(400,"try again");

        }

        [HttpPost("checkPassword")]
        public ActionResult<int> CheckPassword([FromBody] string password)
        {


            int result = _userService.CheckPassword(password);
        
            if (result>-1)
            {
                
                return Ok(result);
            }
            return StatusCode(400, "try again");

        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        //public async void Put(int id, [FromBody]User u)
        //{

        //   await  _userService.UpDate(u,id);

        //}
        public async Task<IActionResult> Put(int id, [FromBody] User u)
        {
            var result = await _userService.UpDate(u, id);
            if (result != null)
                return Ok(result);

            return BadRequest("Could not update user");
        }


        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
