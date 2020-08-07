using StudentResumes.Data.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentResumes.AUTH.Interfaces
{
    public interface IAuthService
    {
        Task<object> Login(string email, string password);

        Task<object> Register(UserDto item);
    }
}
