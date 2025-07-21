using System;

namespace MediTechBackendAPI.Models
{
    public class Prescription
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string PrescribedBy { get; set; } = string.Empty;
    }
}
