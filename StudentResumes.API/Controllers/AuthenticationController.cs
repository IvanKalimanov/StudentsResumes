using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentResumes.API.Models;
using StudentResumes.AUTH.Interfaces;
using StudentResumes.Core.Models;
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
            return new JsonResult(new Response<object>(await _authService.Login(form.Email, form.Password)));
        }

        [HttpPost]
       // [Authorize]
        [Produces(typeof(object))]
        public async Task<ActionResult<object>> Register([FromBody] LoginViewModel item)
        {
            return new JsonResult(new Response<object>(await _authService.Register(new UserDto(item.Email, item.Password))));
        }

    }
}
