using Entities;

using Repositories;

namespace Services
{
    public class UserService
    {
        UserRepository userRepository = new UserRepository();
        public User Register(User user)
        {
            //return userRepository.Register(user);

            List<User> users = userRepository.GetUsers();
            User userfound = users.FirstOrDefault(u => u.userName == user.userName);
            if (userfound == null)
            {
               return userRepository.Register(user);
            }
             return null;
        }
        public User Login(string userName,string password)
        {
        
            User userfound =userRepository.Login(userName) ;
            if (userfound == null)
            {
                return null;
            }
            if(userfound.password == password)
            {
                return userfound;
            }
            return null;
        }
        public User UpDate(User user,int id)
        {

            return userRepository.UpDate(user, id);
        }

    }
}
