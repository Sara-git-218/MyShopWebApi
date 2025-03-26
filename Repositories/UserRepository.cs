

using Entities;

using System.Text.Json;

namespace Repositories
{
    public class UserRepository
    {
        public User Register(User user)
        {
            if (user == null)
            {
                return null;
            }
            try
            {
                int numberOfUsers = System.IO.File.Exists("users.txt") ? System.IO.File.ReadLines("users.txt").Count() : 0;
                user.userId = numberOfUsers + 1;
                if (System.IO.File.Exists("users.txt"))
                {
                    var existingUsers = System.IO.File.ReadLines("users.txt").Select(line => JsonSerializer.Deserialize<User>(line)).ToList();
                    if (existingUsers.Any(u => u.userName == user.userName))
                        return null;// return StatusCode(400, "Username is already taken");
                }
                string userJson = JsonSerializer.Serialize(user);
                System.IO.File.AppendAllText("users.txt", userJson + Environment.NewLine);
                return user;

            }
            catch (Exception ex)
            {
                return null; //return CreatedAtAction(nameof(Get), new { id = user.userId }, user);

            }
        }
        public User Login(User user)
        {
            if (string.IsNullOrEmpty(user?.password) || string.IsNullOrEmpty(user?.userName))
            {
                return null;
            }
            try
            {
                if (!System.IO.File.Exists("users.txt"))
                {
                    return null;
                }
                using (StreamReader reader = System.IO.File.OpenText("users.txt"))
                {
                    string? currentUserInFile;
                    while ((currentUserInFile = reader.ReadLine()) != null)
                    {
                        User u = JsonSerializer.Deserialize<User>(currentUserInFile);
                        if (u.userName == user.userName && u.password == user.password)
                            return user;
                    }
                }
                return null;


            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public void UpDate(User u,int id)
        {
            User newUser = new User();

            if (u.firstName != null)
            {
                newUser.firstName = u.firstName;
            }
            if (u.lastName != null)
            {
                newUser.lastName = u.lastName;
            }
            if (u.password != null)
            {
                newUser.password = u.password;
            }
            if (u.userName != null)
            {
                newUser.userName = u.userName;
            }
            newUser.userId = id;
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "users.txt");


            string textToReplace = string.Empty;
            using (StreamReader reader = System.IO.File.OpenText(filePath))
            {
                string currentUserInFile;
                while ((currentUserInFile = reader.ReadLine()) != null)
                {

                    User user = JsonSerializer.Deserialize<User>(currentUserInFile);
                    if (user.userId == id)
                        textToReplace = currentUserInFile;
                }
            }

            if (textToReplace != string.Empty)
            {
                string text = System.IO.File.ReadAllText(filePath);
                text = text.Replace(textToReplace, JsonSerializer.Serialize(newUser));
                System.IO.File.WriteAllText(filePath, text);
            }
        }
      
       
    }
}
