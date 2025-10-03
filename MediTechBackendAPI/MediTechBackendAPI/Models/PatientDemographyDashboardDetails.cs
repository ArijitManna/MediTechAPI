using System;

namespace MediTechBackendAPI.Models
{
    public class PatientDemographyDashboardDetails
    {
        public string PatientID { get; set; }
        public string PatientFullName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public int Age { get; set; }
    public string Country { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    }
}
