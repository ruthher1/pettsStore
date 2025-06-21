using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    
        public record UserDTO(int Id, string FirstName, string LastName, string Username);
        public record UserRegisterDTO(string FirstName, string LastName, string Password, string Username);
        public record UserLoginDTO(string Password, string Username);
    
}
