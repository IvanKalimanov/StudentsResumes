using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentResumes.API.Models;
using StudentResumes.Data.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentResumes.API.Controllers
{
    [Route("api/[controller]")]
    public class StudentSkillController : Controller
    {
        private readonly IStudentSkillRepository _studentSkillRepository;

        public StudentSkillController(IStudentSkillRepository repo)
        {
            _studentSkillRepository = repo;
        }


        /// <summary>
        /// Get all Student-skill entities
        /// </summary>
        /// <response code="200">Returns student-skill entities</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">If something goes wrong on server</response>
        [SwaggerOperation("GetSkills")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _studentSkillRepository.GetAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Get all Student-skill entities by skill
        /// </summary>
        /// <response code="200">Returns student-skill entities</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">If something goes wrong on server</response>
        [SwaggerOperation("GetStudentSkillBySkillName")]
        [HttpGet("skillname/{skillName}")]
        public async Task<IActionResult> Get(string skillName)
        {
            try
            {
                return Ok(await _studentSkillRepository.GetBySkillNameAsync(skillName));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Get Student-skill entities by student Id
        /// </summary>
        /// <response code="200">Returns student-skill entities</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">If something goes wrong on server</response>
        [SwaggerOperation("GetStudentSkillByStudentId")]
        [HttpGet("studentid/{id}")]
        public async Task<IActionResult> GetByStudentId(Guid id)
        {
            try
            {
                return Ok(await _studentSkillRepository.GetByStudentIdAsync(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Get Students by skill names list
        /// </summary>
        /// <response code="200">Returns student-skill entities</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">If something goes wrong on server</response>
        [SwaggerOperation("SearchStudentsBySkills")]
        [HttpPost("skillsearch")]
        public async Task<IActionResult> SearchStudentsBySkills([FromBody] List<string> skills)
        {
            try
            {
                return Ok(await _studentSkillRepository.SearchBySkillsAsync(skills));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        /// <summary>
        /// Get all Student-skill entities
        /// </summary>
        /// <response code="200">Returns student-skill entities</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">If something goes wrong on server</response>
        [SwaggerOperation("GetSkills")]
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StudentSkillModel studentSkill)
        {
            try
            {
                return Ok(await _studentSkillRepository.SetSkillsToStudentAsync(
                    studentSkill.StudentId, studentSkill.SkillNames));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


    }
}
