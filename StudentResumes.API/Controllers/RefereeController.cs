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
        /// Get all referees
        /// </summary>
        /// <response code="200">Returns all referees</response>
        /// <response code="500">If something goes wrong on server</response>
        [SwaggerOperation("Get")]
        [SwaggerResponse(statusCode: 200, type: typeof(Response<IEnumerable<Referee>>), description: "Access token")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
                return new JsonResult(new Response<IEnumerable<Referee>>(await _repository.GetAsync()));
        }


        /// <summary>
        /// Create new referee
        /// </summary>
        /// <response code="200">Returns new referee</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">If something goes wrong on server</response>
        [SwaggerOperation("Post")]
        [SwaggerResponse(statusCode: 200, type: typeof(Response<Referee>), description: "New entity")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] RefereeCreatingModel referee)
        {
            var result = await _repository.CreateAsync(new RefereeDto(referee.Name, referee.WorkPosition));
            
            return new JsonResult(new Response<Referee>(result));

        }

        /// <summary>
        /// Delete referee by id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Returns true, if entity was deleted succefully</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">If entity was not found</response>
        /// <response code="500">If something goes wrong on server</response>
        [SwaggerOperation("Delete")]
        [SwaggerResponse(statusCode: 200, type: typeof(Response<bool>), description: "Success - true")]
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            return new JsonResult(new Response<bool>(await _repository.DeleteAsync(id)));
        }

        /// <summary>
        /// Edit existing referee
        /// </summary>
        /// <param name="referee"></param>
        /// <response code="200">Returns all referees</response>
        /// <response code="500">If something goes wrong on server</response>
        [SwaggerOperation("Update")]
        [SwaggerResponse(statusCode: 200, type: typeof(Response<Referee>), description: "Updated referee")]
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] RefereeDto referee)
        {
            return new JsonResult(new Response<bool>(await _repository.UpdateAsync(referee)));
        }
    }
}
