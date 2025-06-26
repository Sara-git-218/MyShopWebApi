using AutoMapper;
using DTO;
using Entities;

using Repositories;
using Zxcvbn;
namespace Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository,IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task<UserDTO> Register(UserRegisterDTO user)
        {
            //return userRepository.Register(user);//
            if (CheckPassword(user.Password) < 2)
            {
                return null;
            }
            List<User> users =await  _userRepository.GetUsers();//use GetByUserName insdeatd of GetUsers

            User userfound = users.FirstOrDefault(u => u.UserName.Trim() == user.UserName);
            if (userfound == null)
            {
                User newuser = await _userRepository.Register(_mapper.Map<User>(user));
                return _mapper.Map<UserDTO>(newuser);
            }
            return null;
        }
        public async Task<UserDTO> Login(UserLoginDTO user)
        {

            User userfound = await _userRepository.Login(user.UserName);
            // if (userfound == null)//not needed
            // {
            //     return null;
            // }
            if (userfound.Password.Trim() == user.Password)
            {
                return _mapper.Map<UserDTO>(userfound);
            }
            return null;
        }
        public async Task<UserDTO> Update(User user, int id)
        {
            if (CheckPassword(user.Password) < 2)
            {
                return null;
            }
          
            User userfound = await _userRepository.GetByUserName(user.UserName);//what did you mean by this?
            if (userfound == null||userfound.UserName==user.UserName)
            {
                User newuser = await _userRepository.UpDate(user,id);
                return _mapper.Map<UserDTO>(newuser);
            }
            return null;
           // return  await _userRepository.UpDate(user, id);//
        }
        public int CheckPassword(string password)
        {
            var result = Zxcvbn.Core.EvaluatePassword(password);
            return result.Score;


        }
    }
}
