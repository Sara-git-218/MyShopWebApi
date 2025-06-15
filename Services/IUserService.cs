using DTO;
using Entities;

namespace Services
{
    public interface IUserService
    {
        int CheckPassword(string password);
        Task<UserDTO> Login(UserLoginDTO user);
        Task<UserDTO> Register(UserRegisterDTO user);
       Task<UserDTO> UpDate(User user, int id);
    }
}