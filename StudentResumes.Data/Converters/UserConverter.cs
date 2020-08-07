using StudentResumes.Data.Dto;
using StudentResumes.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentResumes.Data.Converters
{
    public class UserConverter
    {
        public static User Convert(UserDto item) =>
        new User
        {
            Email = item.Email,
            UserName = item.Email,
            Id = item.Id
        };

        public static UserDto Convert(User item) =>
            new UserDto()
            {
                Email = item.Email,
                Id = item.Id
            };

        public static List<User> Convert(List<UserDto> items) =>
            items.Select(u => Convert(u)).ToList();

        public static List<UserDto> Convert(List<User> items) =>
            items.Select(u => Convert(u)).ToList();
    }
}
