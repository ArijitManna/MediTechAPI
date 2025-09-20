using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediTechBackendAPI.Models;
using MediTechBackendAPI.Repositories;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MediTechBackendAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DependentsController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;

        public DependentsController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        // POST: api/Dependents
        [HttpPost]
        public async Task<IActionResult> InsertDependent([FromBody] Dependent dependent)
        {
            var pidClaim = User.FindFirst("PID")?.Value;
            if (pidClaim == null || !Guid.TryParse(pidClaim, out Guid pid))
                return Unauthorized();

            dependent.PID = pid;
            var result = await _patientRepository.InsertDependentAsync(dependent);
            if (result)
                return Ok(new { message = "Dependent inserted successfully." });
            return BadRequest(new { message = "Failed to insert dependent." });
        }

        // GET: api/Dependents
        [HttpGet]
        public async Task<IActionResult> GetDependents()
        {
            var pidClaim = User.FindFirst("PID")?.Value;
            if (pidClaim == null || !Guid.TryParse(pidClaim, out Guid pid))
                return Unauthorized();

            var dependents = await _patientRepository.GetDependentsAsync(pid);
            return Ok(dependents);
        }
    }
}
