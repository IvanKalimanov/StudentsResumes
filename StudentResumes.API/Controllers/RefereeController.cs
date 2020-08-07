using Microsoft.AspNetCore.Mvc;
using StudentResumes.Data.Entities;
using StudentResumes.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using StudentResumes.Data.Dto;
using StudentResumes.API.Models;

namespace StudentResumes.API.Controllers
{
    [Route("api/[controller]")]
    public class RefereeController : Controller
    {
        private readonly IRefereeRepository _repository;

        public RefereeController(IRefereeRepository repository)
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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] RefereeCreatingModel referee)
        {
            try
            {
                return Ok(await _repository.CreateAsync(new RefereeDto(referee.Name, referee.WorkPosition)));
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
        public async Task<IActionResult> Update([FromBody] RefereeDto referee)
        {
            try
            {
                return Ok(await _repository.UpdateAsync(referee));
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}
