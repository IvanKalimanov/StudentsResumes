using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class StudentController : Controller
    {
        private readonly IStudentRepository _repository;

        public StudentController(IStudentRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get all students
        /// </summary>
        /// <response code="200">Returns all students</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">If something goes wrong on server</response>
        [SwaggerOperation("Get")]
        [SwaggerResponse(statusCode: 200, type: typeof(Response<IEnumerable<StudentDto>>), description: "Students")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
                return new JsonResult(new Response<IEnumerable<StudentDto>>(await _repository.GetAsync()));
        }

        /// <summary>
        /// Search students by name
        /// </summary>
        /// <param name="name"></param>
        /// <response code="200">Returns all students</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">If something goes wrong on server</response>
        [SwaggerOperation("SearchByName")]
        [SwaggerResponse(statusCode: 200, type: typeof(Response<ICollection<StudentDto>>), description: "Students with the same name")]
        [HttpGet("byname/{name}")]
        public async Task<IActionResult> SearchByName(string name)
        {
            return new JsonResult(new Response<ICollection<StudentDto>>(await _repository.SearchByNameAsync(name)));
        }

        /// <summary>
        /// Create student
        /// </summary>
        /// <param name="student"></param>
        /// <param name="file"></param>
        /// <response code="200">Returns new student</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">If something goes wrong on server</response>
        [SwaggerOperation("Post")]
        [SwaggerResponse(statusCode: 200, type: typeof(Response<StudentDto>), description: "New student")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] StudentDto student, IFormFile file)
        {
            return new JsonResult(new Response<StudentDto>(await _repository.CreateAsync(student, file)));
        }


        /// <summary>
        /// Upload resume file
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="file"></param>
        /// <response code="200">Returns true if success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">If entity was not found</response>
        /// <response code="500">If something goes wrong on server</response>
        [SwaggerOperation("UploadResume")]
        [SwaggerResponse(statusCode: 200, type: typeof(Response<bool>), description: "True if success")]
        [HttpPost("upload/{studentId}")]
        [Authorize]
        public async Task<IActionResult> UploadResume(Guid studentId, IFormFile file)
        {
            return new JsonResult(new Response<bool>(await _repository.UploadResumeFileAsync(file, studentId)));
        }

        /// <summary>
        /// Delete student
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Returns true if success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">If entity was not found</response>
        /// <response code="500">If something goes wrong on server</response>
        [SwaggerOperation("Delete")]
        [SwaggerResponse(statusCode: 200, type: typeof(Response<bool>), description: "True if success")]
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            return new JsonResult(new Response<bool>(await _repository.DeleteAsync(id)));
        }


        /// <summary>
        /// Edit student
        /// </summary>
        /// <param name="student"></param>
        /// <response code="200">Returns true if success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">If entity was not found</response>
        /// <response code="500">If something goes wrong on server</response>
        [SwaggerOperation("Delete")]
        [SwaggerResponse(statusCode: 200, type: typeof(Response<bool>), description: "True if success")]
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] StudentDto student)
        {
            return new JsonResult(new Response<bool>(await _repository.UpdateAsync(student)));
        }
    }
}
