using System;

namespace MediTechBackendAPI.Models
{
	public class PatientRegistration
	{
		public string CountryCode { get; set; } = string.Empty;
		public string MobileNumber { get; set; } = string.Empty;
		public string? EmailID { get; set; }
		public string? FirstName { get; set; }
		public string? MiddleName { get; set; }
		public string? LastName { get; set; }
		public int? Age { get; set; }
		public DateTime? Dob { get; set; }
		public string? Gender { get; set; }
		public int? CountryID { get; set; }
		public int? StateID { get; set; }
		public int? DistrictID { get; set; }
		public int? CityID { get; set; }
		public string? CreatedBy { get; set; }
	}
}
