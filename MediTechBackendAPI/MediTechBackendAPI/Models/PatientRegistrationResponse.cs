using System;

namespace MediTechBackendAPI.Models
{
	public class PatientRegistrationResponse
	{
		public Guid GeneratedPID { get; set; }
		public string GeneratedOTP { get; set; } = string.Empty;
		public string OtpValidTime { get; set; } = string.Empty;
	}
}
