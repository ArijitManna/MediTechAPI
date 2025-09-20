
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediTechBackendAPI.Models;
using MediTechBackendAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Security.Claims;

namespace MediTechBackendAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientRepository _repository;

        public PatientsController(IPatientRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(int id)
        {
            var patient = await _repository.GetPatientAsync(id);
            if (patient == null) return NotFound();
            return Ok(patient);
        }

        [HttpGet("{id}/health-records")]
        public async Task<ActionResult<IEnumerable<HealthRecord>>> GetHealthRecords(int id)
        {
            var records = await _repository.GetHealthRecordsAsync(id);
            return Ok(records);
        }

        [HttpGet("{id}/overall-report")]
        public async Task<ActionResult<OverallReport>> GetOverallReport(int id)
        {
            var report = await _repository.GetOverallReportAsync(id);
            if (report == null) return NotFound();
            return Ok(report);
        }

        [HttpGet("{id}/notifications")]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotifications(int id)
        {
            var notifications = await _repository.GetNotificationsAsync(id);
            return Ok(notifications);
        }

        [HttpGet("{id}/appointments")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments(int id)
        {
            var list = await _repository.GetAppointmentsAsync(id);
            return Ok(list);
        }

        [HttpGet("{id}/dependants")]
        public async Task<ActionResult<IEnumerable<Dependent>>> GetDependants(int id)
        {
            var list = await _repository.GetDependantsAsync(id);
            return Ok(list);
        }

        [HttpGet("{id}/medical-records")]
        public async Task<ActionResult<IEnumerable<MedicalRecord>>> GetMedicalRecords(int id)
        {
            var list = await _repository.GetMedicalRecordsAsync(id);
            return Ok(list);
        }

        [HttpGet("{id}/prescriptions")]
        public async Task<ActionResult<IEnumerable<Prescription>>> GetPrescriptions(int id)
        {
            var list = await _repository.GetPrescriptionsAsync(id);
            return Ok(list);
        }

        // New endpoint: GET api/Patients/dashboard-details (JWT required)
        [HttpGet("dashboard-details")]
        [Authorize]
        public async Task<ActionResult<Models.PatientDemographyDashboardDetails>> GetPatientDemographyDashboardDetails()
        {
            var pidClaim = User.Claims.FirstOrDefault(c => c.Type == "PID");
            if (pidClaim == null || !Guid.TryParse(pidClaim.Value, out var pid))
            {
                return Unauthorized("PID not found in token");
            }
            var details = await _repository.GetPatientDemographyDashboardDetailsAsync(pid);
            if (details == null) return NotFound();
            return Ok(details);
        }
    }
}
