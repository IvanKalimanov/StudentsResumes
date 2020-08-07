using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using StudentResumes.AUTH.Interfaces;
using StudentResumes.Core.EF;
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
            try
            {
                if (email == null || password == null)
                    return null;

                var result = await _signInManager.PasswordSignInAsync(email, password, false, false);

                if (result.Succeeded)
                {
                    var appUser = await _userManager.FindByEmailAsync(email);
                    return await _jwt.GenerateJwt(appUser);
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<object> Register(UserDto item)
        {
            try
            {
                User user = UserConverter.Convert(item);
                if (user == null)
                    return null;

                var result = await _userManager.CreateAsync(user, item.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "admin");
                    await _signInManager.SignInAsync(user, false);
                    return await _jwt.GenerateJwt(user);
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
