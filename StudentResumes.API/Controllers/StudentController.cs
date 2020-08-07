using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            try
            {
                return Ok(await _repository.GetAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Get Statistics
        /// </summary>
        [SwaggerOperation("SendGeneralMessage")]
        [HttpGet("byname/{name}")]
        public async Task<IActionResult> SearchByName(string name)
        {
            try
            {
                return Ok(await _repository.SearchByNameAsync(name));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] StudentDto student)
        {
            try
            {
                return Ok(await _repository.CreateAsync(student));
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost("upload/{studentId}")]
        [Authorize]
        public async Task<IActionResult> UploadResume(Guid studentId, IFormFile file)
        {
            try
            {
                return Ok(await _repository.UploadResumeFileAsync(file, studentId));
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                return Ok(await _repository.DeleteAsync(id));
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] StudentDto student)
        {
            try
            {
                return Ok(await _repository.UpdateAsync(student));
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}
