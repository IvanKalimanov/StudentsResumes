﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentResumes.Core.Models;
using StudentResumes.Data.Entities;
using StudentResumes.Data.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentResumes.API.Controllers
{
    [Route("api/[controller]")]
    public class SkillController : Controller
    {
        private readonly ISkillRepository _skillRepository;

        public SkillController(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }

        /// <summary>
        /// Get all skills
        /// </summary>
        /// <response code="200">Returns skills</response>
        /// <response code="500">If something goes wrong on server</response>
        [SwaggerResponse(statusCode: 200, type: typeof(Response<IEnumerable<string>>), description: "Skills as strings")]
        [SwaggerOperation("Get")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return new JsonResult(new Response<IEnumerable<string>>(await _skillRepository.GetAsync()));
        }

        /// <summary>
        /// Create new skill
        /// </summary>
        /// <param name="name"></param>
        /// <response code="200">Returns new skill</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">If something goes wrong on server</response>
        [SwaggerOperation("Post")]
        [SwaggerResponse(statusCode: 200, type: typeof(Response<Skill>), description: "New skill")]
        [Authorize]
        [HttpPost("{name}")]
        public async Task<IActionResult> Post(string name)
        {
            return new JsonResult(new Response<Skill>(await _skillRepository.CreateAsync(name)));
        }

        /// <summary>
        /// Delete skill by name
        /// </summary>
        /// <param name="name"></param>
        /// <response code="200">Returns true, if skill was deleted succefully</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">If entity was not found</response>
        /// <response code="500">If something goes wrong on server</response>
        [SwaggerOperation("Delete")]
        [SwaggerResponse(statusCode: 200, type: typeof(Response<bool>), description: "True if success")]
        [Authorize]
        [HttpDelete("{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            return new JsonResult(new Response<bool>(await _skillRepository.DeleteAsync(name)));
        }

    }
}
