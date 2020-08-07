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
using StudentResumes.Core.Models;

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
                return new JsonResult(new Response<IEnumerable<Referee>>(await _repository.GetAsync()));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] RefereeCreatingModel referee)
        {
            var result = await _repository.CreateAsync(new RefereeDto(referee.Name, referee.WorkPosition));
            
            return new JsonResult(new Response<Referee>(result));

        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            return new JsonResult(new Response<bool>(await _repository.DeleteAsync(id)));
        }
           

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] RefereeDto referee)
        {
            return new JsonResult(new Response<bool>(await _repository.UpdateAsync(referee)));
        }
    }
}
