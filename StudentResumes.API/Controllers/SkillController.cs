using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        /// <response code="401">Unauthorized</response>
        /// <response code="500">If something goes wrong on server</response>
        [SwaggerOperation("GetSkills")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _skillRepository.GetAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Create new skill
        /// </summary>
        /// <param name="name"></param>
        /// <response code="200">Returns new skill</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">If something goes wrong on server</response>
        [SwaggerOperation("CreateSkill")]
       // [Authorize]
        [HttpPost("{name}")]
        public async Task<IActionResult> Post(string name)
        {
            try
            {
                return Ok(await _skillRepository.CreateAsync(name));
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        /// <summary>
        /// Delete skill by name
        /// </summary>
        /// <param name="name"></param>
        /// <response code="200">Returns true</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">If something goes wrong on server</response>
        [SwaggerOperation("DeleteSkill")]
        [Authorize]
        [HttpDelete("{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            try
            {
                return Ok(await _skillRepository.DeleteAsync(name));
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

    }
}
