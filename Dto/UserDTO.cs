using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
 
    public record UserDTO(int Id,string UserName,string FirstName, string LastName);
    public record UserLoginDTO(string UserName,string Password);
    public record UserRegisterDTO(string UserName,string Password,string FirstName,string LastName);
  
}
