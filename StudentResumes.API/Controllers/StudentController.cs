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
        /// Get Statistics
        /// </summary>
        [SwaggerOperation("SendGeneralMessage")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
                return new JsonResult(new Response<IEnumerable<StudentDto>>(await _repository.GetAsync()));
        }

        /// <summary>
        /// Get Statistics
        /// </summary>
        [SwaggerOperation("SendGeneralMessage")]
        [HttpGet("byname/{name}")]
        public async Task<IActionResult> SearchByName(string name)
        {
                return new JsonResult(new Response<ICollection<StudentDto>>(await _repository.SearchByNameAsync(name)));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] StudentDto student)
        {
                return new JsonResult(new Response<StudentDto>(await _repository.CreateAsync(student)));
        }

        [HttpPost("upload/{studentId}")]
        [Authorize]
        public async Task<IActionResult> UploadResume(Guid studentId, IFormFile file)
        {
                return new JsonResult(new Response<bool>(await _repository.UploadResumeFileAsync(file, studentId)));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
                return new JsonResult(new Response<bool>(await _repository.DeleteAsync(id)));
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] StudentDto student)
        {
                return new JsonResult(new Response<bool>(await _repository.UpdateAsync(student)));
        }
    }
}
