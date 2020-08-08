using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using StudentResumes.AUTH.Interfaces;
using StudentResumes.Core.EF;
using StudentResumes.Core.Exceptions;
using StudentResumes.Data.Converters;
using StudentResumes.Data.Dto;
using StudentResumes.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentResumes.AUTH.Services
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IJwtGenerator _jwt;
        private readonly ResumesContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(SignInManager<User> sim, UserManager<User> um, IJwtGenerator jwt, ResumesContext context,
            IConfiguration configuration)
        {
            _signInManager = sim;
            _userManager = um;
            _jwt = jwt;
            _context = context;
            _configuration = configuration;
        }

        public async Task<object> Login(string email, string password)
        {
            if (email == null || password == null)
                throw new InvalidLoginOrPasswordException();

            var result = await _signInManager.PasswordSignInAsync(email, password, false, false);

            if (!result.Succeeded)
                throw new InvalidLoginOrPasswordException();

            var appUser = await _userManager.FindByEmailAsync(email);

            return await _jwt.GenerateJwt(appUser);

        }

        public async Task<object> Register(UserDto item)
        {
            User user = UserConverter.Convert(item);
            if (user == null)
                throw new ArgumentNullException();

            var result = await _userManager.CreateAsync(user, item.Password);

            if (!result.Succeeded)
                throw new InvalidLoginOrPasswordException();

            await _userManager.AddToRoleAsync(user, "admin");
            await _signInManager.SignInAsync(user, false);
            return await _jwt.GenerateJwt(user);

        }
    }
}
