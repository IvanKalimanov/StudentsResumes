using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentResumes.API.Models;
using StudentResumes.Core.Models;
using StudentResumes.Data.Dto;
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
            return new JsonResult(new Response<ICollection<StudentSkill>>(await _studentSkillRepository.GetAsync()));
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
            var result = await _studentSkillRepository.GetBySkillNameAsync(skillName);

            return new JsonResult(new Response<ICollection<StudentSkill>>(result));
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
            var result = await _studentSkillRepository.GetByStudentIdAsync(id);

            return new JsonResult(new Response<ICollection<StudentSkill>>(result));
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
            var result = await _studentSkillRepository.SearchBySkillsAsync(skills);

            return new JsonResult(new Response<ICollection<StudentDto>>(result));
        }


        /// <summary>
        /// Get all Student-skill entities
        /// </summary>
        /// <response code="200">Returns student-skill entities</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">If something goes wrong on server</response>
        [SwaggerOperation("GetSkills")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StudentSkillModel studentSkill)
        {
            var result = await _studentSkillRepository.SetSkillsToStudentAsync(
                    studentSkill.StudentId, studentSkill.SkillNames);

            return new JsonResult(new Response<ICollection<StudentSkill>>(result));

        }


    }
}
