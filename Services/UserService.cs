using Entities;

using Repositories;

namespace Services
{
    public class UserService
    {
        UserRepository userRepository = new UserRepository();
        public User Register(User user)
        {
            return userRepository.Register(user);
        }
        public User Login(User user)
        {
            return userRepository.Login(user);
        }
        public void UpDate(User user,int id)
        {
            userRepository.UpDate(user, id);
        }

    }
}
