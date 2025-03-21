using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShopWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
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
        public ActionResult<User> Register([FromBody] User user)
        {

            if (user == null)
            {
                return StatusCode(400, "username  and password are required");
            }
            try
            {
                int numberOfUsers = System.IO.File.Exists("users.txt") ? System.IO.File.ReadLines("users.txt").Count() : 0;
                user.UserId = numberOfUsers + 1;
                if (System.IO.File.Exists("users.txt"))
                {
                    var existingUsers = System.IO.File.ReadLines("users.txt").Select(line => JsonSerializer.Deserialize<User>(line)).ToList();
                    if (existingUsers.Any(u => u.user_name == user.user_name))
                        return StatusCode(400, "Username is already taken");
                }
                string userJson = JsonSerializer.Serialize(user);
                System.IO.File.AppendAllText("users.txt", userJson + Environment.NewLine);
                return CreatedAtAction(nameof(Get), new { id = user.UserId }, user);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error writing user to file: " + ex.Message);
            }


        }

        [HttpPost("login")]
        public ActionResult<User> Login([FromBody] User user)
        {

            if (string.IsNullOrEmpty(user?.password) || string.IsNullOrEmpty(user?.user_name))
            {
                return StatusCode(400, "username  and password are required");
            }
            try
            {
                if (!System.IO.File.Exists("users.txt"))
                {
                    return NotFound("No users found.");
                }
                using (StreamReader reader = System.IO.File.OpenText("users.txt"))
                {
                    string? currentUserInFile;
                    while ((currentUserInFile = reader.ReadLine()) != null)
                    {
                        User u = JsonSerializer.Deserialize<User>(currentUserInFile);
                        if (u.user_name == user.user_name && u.password == user.password)
                            return Ok(user);
                    }
                }
                return Unauthorized("Invalid username or password.");


            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error writing user to file: " + ex.Message);
            }


        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
