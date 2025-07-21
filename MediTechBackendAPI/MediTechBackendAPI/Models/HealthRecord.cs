using System;

namespace MediTechBackendAPI.Models
{
    public class HealthRecord
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int HeartRate { get; set; }
        public decimal Temperature { get; set; }
        public decimal GlucoseLevel { get; set; }
        public decimal SpO2 { get; set; }
        public int BloodPressure { get; set; }
        public decimal BMI { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
