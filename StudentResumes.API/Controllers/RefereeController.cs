using Microsoft.AspNetCore.Mvc;
using StudentResumes.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
