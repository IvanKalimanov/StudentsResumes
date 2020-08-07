using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentResumes.API.Models;
using StudentResumes.AUTH.Interfaces;
using StudentResumes.Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentResumes.API.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AuthenticationController : Controller
    {
        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Produces(typeof(object))]
        public async Task<ActionResult<object>> Login([FromBody] LoginViewModel form)
        {
            try
            {
                return await _authService.Login(form.Email, form.Password);
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex);
            }
        }

        [HttpPost]
       // [Authorize]
        [Produces(typeof(object))]
        public async Task<ActionResult<object>> Register([FromBody] LoginViewModel item)
        {
            try
            {
                return await _authService.Register(new UserDto(item.Email, item.Password));
            }
            catch (Exception ex)
            {
                return StatusCode(520, ex);
            }
        }

    }
}
