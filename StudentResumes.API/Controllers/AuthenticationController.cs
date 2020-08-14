using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentResumes.API.Models;
using StudentResumes.AUTH.Interfaces;
using StudentResumes.Core.Models;
using StudentResumes.Data.Dto;
using Swashbuckle.AspNetCore.Annotations;
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

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="form"></param>
        /// <response code="200">User successfully logged in</response>
        /// <response code="500">If something goes wrong on server or invalid password (login)</response>
        [SwaggerOperation("Login")]
        [SwaggerResponse(statusCode: 200, type: typeof(Response<object>), description: "Access token")]
        [HttpPost]
        [Produces(typeof(object))]
        public async Task<ActionResult<object>> Login([FromBody] LoginViewModel form)
        {
            return new JsonResult(new Response<object>(await _authService.Login(form.Email, form.Password)));
        }


        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="item"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /register
        ///     {
        ///        "Email": "example@ex.ru",
        ///        "Password": "asdaswqer123"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">User successfully registered</response>
        /// <response code="500">If something goes wrong on server or invalid password (login)</response>
        [SwaggerOperation("Register")]
        [SwaggerResponse(statusCode: 200, type: typeof(Response<object>), description: "Access token")]
        [HttpPost]
        //[Authorize]
        [Produces(typeof(object))]
        public async Task<ActionResult<object>> Register([FromBody] LoginViewModel item)
        {
            return new JsonResult(new Response<object>(await _authService.Register(new UserDto(item.Email, item.Password))));
        }

    }
}
