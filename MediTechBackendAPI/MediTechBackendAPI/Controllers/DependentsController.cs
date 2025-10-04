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
                return BadRequest(ApiResponse<object>.Failure("Invalid or missing PID in request body."));

            if (request.Dependents == null)
                return BadRequest(ApiResponse<object>.Failure("Dependents list is required."));

            try
            {
                var result = await _patientRepository.InsertOrUpdateDependentsAsync(pid, request.PID, request.Created_by, request.Dependents);
                if (result)
                    return Ok(ApiResponse<object>.Success(new { }, "Dependents inserted/updated successfully."));
                return BadRequest(ApiResponse<object>.Failure("Failed to insert/update dependents."));
            }
            catch (System.Exception)
            {
                return StatusCode(500, ApiResponse<object>.Failure("An unexpected error occurred"));
            }
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
            public required string PID { get; set; }
            public required string Created_by { get; set; }
            public required System.Collections.Generic.List<Dependent> Dependents { get; set; }
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

        // PATCH: api/Dependents/{dependentId}/status
        [HttpPatch("{dependentId}/status")]
        public async Task<IActionResult> UpdateDependentStatus(Guid dependentId, [FromBody] UpdateDependentStatusRequest request)
        {
            var pidClaim = User.FindFirst("PID")?.Value;
            if (pidClaim == null || !Guid.TryParse(pidClaim, out Guid pid))
                return Unauthorized();

            if (dependentId == Guid.Empty)
            {
                return BadRequest(ApiResponse<object>.Failure("Invalid dependent ID"));
            }

            try
            {
                var (success, message) = await _patientRepository.UpdateDependentStatusAsync(dependentId, request.UpdatedBy, request.Status);
                
                if (success)
                    return Ok(ApiResponse<object>.Success(new { }, message));
                
                return BadRequest(ApiResponse<object>.Failure(message));
            }
            catch (System.Exception)
            {
                return StatusCode(500, ApiResponse<object>.Failure("An unexpected error occurred"));
            }
        }

        // DELETE: api/Dependents/{dependentId}
        [HttpDelete("{dependentId}")]
        public async Task<IActionResult> DeleteDependent(Guid dependentId, [FromBody] DeleteDependentRequest request)
        {
            var pidClaim = User.FindFirst("PID")?.Value;
            if (pidClaim == null || !Guid.TryParse(pidClaim, out Guid pid))
                return Unauthorized();

            if (dependentId == Guid.Empty)
            {
                return BadRequest(ApiResponse<object>.Failure("Invalid dependent ID"));
            }

            try
            {
                var (success, message) = await _patientRepository.DeleteDependentAsync(dependentId, request.DeletedBy);
                
                if (success)
                    return Ok(ApiResponse<object>.Success(new { }, message));
                
                return BadRequest(ApiResponse<object>.Failure(message));
            }
            catch (System.Exception)
            {
                return StatusCode(500, ApiResponse<object>.Failure("An unexpected error occurred"));
            }
        }

        public class UpdateDependentStatusRequest
        {
            public required string UpdatedBy { get; set; }
            public required bool Status { get; set; }
        }

        public class DeleteDependentRequest
        {
            public required string DeletedBy { get; set; }
        }
    }
}
