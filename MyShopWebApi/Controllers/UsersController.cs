using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Services;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShopWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        UserService userService = new UserService();
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

            //if (user == null)
            //{
            //    return StatusCode(400, "username  and password are required");
            //}
            //try
            //{
            //    int numberOfUsers = System.IO.File.Exists("users.txt") ? System.IO.File.ReadLines("users.txt").Count() : 0;
            //    user.userId = numberOfUsers + 1;
            //    if (System.IO.File.Exists("users.txt"))
            //    {
            //        var existingUsers = System.IO.File.ReadLines("users.txt").Select(line => JsonSerializer.Deserialize<User>(line)).ToList();
            //        if (existingUsers.Any(u => u.userName == user.userName))
            //            return StatusCode(400, "Username is already taken");
            //    }
            //    string userJson = JsonSerializer.Serialize(user);
            //    System.IO.File.AppendAllText("users.txt", userJson + Environment.NewLine);
            //    return CreatedAtAction(nameof(Get), new { id = user.userId }, user);

            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, "Error writing user to file: " + ex.Message);
            //}
            User u = userService.Register(user);
            if (u!=null)
            {
                return Ok(u);
            }
            return StatusCode(400,"try Again");


        }

        [HttpPost("login")]
        public ActionResult<User> Login([FromBody] User user)
        {

            //if (string.IsNullOrEmpty(user?.password) || string.IsNullOrEmpty(user?.userName))
            //{
            //    return StatusCode(400, "username  and password are required");
            //}
            //try
            //{
            //    if (!System.IO.File.Exists("users.txt"))
            //    {
            //        return NotFound("No users found.");
            //    }
            //    using (StreamReader reader = System.IO.File.OpenText("users.txt"))
            //    {
            //        string? currentUserInFile;
            //        while ((currentUserInFile = reader.ReadLine()) != null)
            //        {
            //            User u = JsonSerializer.Deserialize<User>(currentUserInFile);
            //            if (u.userName == user.userName && u.password == user.password)
            //                return Ok(u);
            //        }
            //    }
            //    return Unauthorized("Invalid username or password.");


            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, "Error writing user to file: " + ex.Message);
            //}
            User u = userService.Login(user.userName,user.password);
            if (u!=null)
            {
                return Ok(u);
            }
            return StatusCode(400,"try again");

        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]User u)
        {
            //User newUser=new User();

            //if(u.firstName!=null)
            //{
            //    newUser.firstName = u.firstName;
            //}
            //if (u.lastName != null)
            //{
            //    newUser.lastName = u.lastName;
            //}
            //if (u.password != null)
            //{
            //    newUser.password = u.password;
            //}
            //if (u.userName != null)
            //{
            //    newUser.userName = u.userName;
            //}
            //newUser.userId = id;
            //string filePath = Path.Combine(Directory.GetCurrentDirectory(), "users.txt");


            //string textToReplace = string.Empty;
            //using (StreamReader reader = System.IO.File.OpenText(filePath))
            //{
            //    string currentUserInFile;
            //    while ((currentUserInFile = reader.ReadLine()) != null)
            //    {

            //        User user = JsonSerializer.Deserialize<User>(currentUserInFile);
            //        if (user.userId == id)
            //            textToReplace = currentUserInFile;
            //    }
            //}

            //if (textToReplace != string.Empty)
            //{
            //    string text = System.IO.File.ReadAllText(filePath);
            //    text = text.Replace(textToReplace, JsonSerializer.Serialize(newUser));
            //    System.IO.File.WriteAllText(filePath, text);
            //}

            userService.UpDate(u,id);

        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
