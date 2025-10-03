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


        // POST: api/Dependents/bulk
        [HttpPost("bulk")]
        public async Task<IActionResult> InsertOrUpdateDependents([FromBody] BulkDependentsRequest request)
        {
            var pidClaim = User.FindFirst("PID")?.Value;
            if (pidClaim == null || !Guid.TryParse(pidClaim, out Guid userPid))
                return Unauthorized();

            if (string.IsNullOrEmpty(request.PID) || !Guid.TryParse(request.PID, out Guid pid))
                return BadRequest(new { message = "Invalid or missing PID in request body." });

            if (request.Dependents == null)
                return BadRequest(new { message = "Dependents list is required." });

            var result = await _patientRepository.InsertOrUpdateDependentsAsync(pid, request.PID, request.Created_by, request.Dependents);
            if (result)
                return Ok(new { message = "Dependents inserted/updated successfully." });
            return BadRequest(new { message = "Failed to insert/update dependents." });
        }

        // For backward compatibility, keep single insert endpoint
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

        public class BulkDependentsRequest
        {
            public string PID { get; set; }
            public string Created_by { get; set; }
            public System.Collections.Generic.List<Dependent> Dependents { get; set; }
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
