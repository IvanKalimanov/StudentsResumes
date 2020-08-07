using System;
using System.Collections.Generic;
using System.Text;

namespace StudentResumes.Data.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public UserDto(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public UserDto()
        {
        }
    }
}
