

using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Text.Json;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        _326059268_ShopApiContext DBcontext;
        public UserRepository(_326059268_ShopApiContext DBcontext)
        {
            this.DBcontext = DBcontext;
        }
        public async Task<List<User>> GetUsers()
        {
       
            return await DBcontext.Users.ToListAsync();
        }
        public async Task<User> Register(User user)
        {
       
           await DBcontext.Users.AddAsync(user);
            await DBcontext.SaveChangesAsync();
            return await Task.FromResult(user);
         
        }
        public async  Task<User> Login(string userName)//(User user)
        {
           
            User userLog =await DBcontext.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            return await Task.FromResult(userLog);
          
        }
        public async Task<User> UpDate(User user, int id)
        {

            DBcontext.Users.Update(user);
            await DBcontext.SaveChangesAsync();
            return await Task.FromResult(user);
          
        }
        


    }
}
