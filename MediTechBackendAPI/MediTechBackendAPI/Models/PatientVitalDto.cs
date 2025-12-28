using System;

namespace MediTechBackendAPI.Models
{
    public class PatientVitalDto
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public decimal? BMI { get; set; }
        public int? HeartRate { get; set; }
        public decimal? Weight { get; set; }
        public string FBC { get; set; }
        public decimal? Glucose { get; set; }
        public decimal? Temperature { get; set; }
        public int? SpO2 { get; set; }
        public int? Systolic { get; set; }
        public int? Diastolic { get; set; }
        public DateTime? RecordedOn { get; set; }
    }
}
