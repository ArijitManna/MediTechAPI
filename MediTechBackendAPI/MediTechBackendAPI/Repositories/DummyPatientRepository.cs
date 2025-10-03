
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediTechBackendAPI.Models;


namespace MediTechBackendAPI.Repositories
{
	public class DummyPatientRepository : IPatientRepository
	{
		public Task<Patient?> GetPatientAsync(int patientId)
		{
			var patient = new Patient
			{
				Id = patientId,
				Name = "John Doe",
				Gender = "Male",
				Age = 40
			};
			return Task.FromResult<Patient?>(patient);
		}

		public Task<PatientDemographyDashboardDetails?> GetPatientDemographyDashboardDetailsAsync(Guid pid)
		{
			var details = new PatientDemographyDashboardDetails
			{
				PatientID = "1",
				PatientFullName = "Hendrita Hayes",
				Gender = "Female",
				Age = 32,
				Country = "India",
				ImageUrl = "/images/female.png"
			};
			if (details.Gender.Equals("male", StringComparison.OrdinalIgnoreCase))
				details.ImageUrl = "/images/male.png";
			else if (details.Gender.Equals("female", StringComparison.OrdinalIgnoreCase))
				details.ImageUrl = "/images/female.png";
			else
				details.ImageUrl = "/images/male.png";
			return Task.FromResult<PatientDemographyDashboardDetails?>(details);
		}
		public Task<IEnumerable<Dependent>> GetDependentsAsync(Guid pid)
		{
			var dependents = new List<Dependent>
			{
				new Dependent
				{
					Dependent_ID = Guid.NewGuid(),
					PID = pid,
					PatientID = "P123",
					First_Name = "John",
					Middle_Name = "",
					Last_Name = "Doe",
					Age = 30,
					RelationshipID = 1,
					Created_by = "dummy",
					D_Created_At = DateTime.UtcNow,
					Modified_by = null,
					D_Modified_At = null,
					ImageUrl = "/images/male.png"
				},
				new Dependent
				{
					Dependent_ID = Guid.NewGuid(),
					PID = pid,
					PatientID = "P123",
					First_Name = "Laura",
					Middle_Name = "",
					Last_Name = "Smith",
					Age = 28,
					RelationshipID = 2,
					Created_by = "dummy",
					D_Created_At = DateTime.UtcNow.AddDays(-10),
					Modified_by = null,
					D_Modified_At = null,
					ImageUrl = "/images/female.png"
				}
			};
			return Task.FromResult<IEnumerable<Dependent>>(dependents);
		}

		public Task<bool> InsertOrUpdateDependentsAsync(System.Guid pid, string patientId, string createdBy, IEnumerable<Dependent> dependents)
		{
			// Always return true for dummy implementation
			return Task.FromResult(true);
		}

		public Task<IEnumerable<HealthRecord>> GetHealthRecordsAsync(int patientId)
		{
			return Task.FromResult<IEnumerable<HealthRecord>>(new List<HealthRecord>());
		}

		public Task<OverallReport?> GetOverallReportAsync(int patientId)
		{
			return Task.FromResult<OverallReport?>(null);
		}

		public Task<IEnumerable<Notification>> GetNotificationsAsync(int patientId)
		{
			return Task.FromResult<IEnumerable<Notification>>(new List<Notification>());
		}

		public Task<IEnumerable<Appointment>> GetAppointmentsAsync(int patientId)
		{
			return Task.FromResult<IEnumerable<Appointment>>(new List<Appointment>());
		}

		public Task<IEnumerable<Dependent>> GetDependantsAsync(int patientId)
		{
			return Task.FromResult<IEnumerable<Dependent>>(new List<Dependent>());
		}

		public Task<IEnumerable<MedicalRecord>> GetMedicalRecordsAsync(int patientId)
		{
			return Task.FromResult<IEnumerable<MedicalRecord>>(new List<MedicalRecord>());
		}

		public Task<IEnumerable<Prescription>> GetPrescriptionsAsync(int patientId)
		{
			return Task.FromResult<IEnumerable<Prescription>>(new List<Prescription>());
		}

		public Task<bool> InsertDependentAsync(Dependent dependent)
		{
			return Task.FromResult(true);
		}
	}
}

