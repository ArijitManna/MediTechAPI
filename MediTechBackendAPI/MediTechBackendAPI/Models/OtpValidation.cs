using System;

namespace MediTechBackendAPI.Models
{
	public class OtpValidation
	{
		public Guid PID { get; set; }
		public string OTP { get; set; } = string.Empty;
	}
}
