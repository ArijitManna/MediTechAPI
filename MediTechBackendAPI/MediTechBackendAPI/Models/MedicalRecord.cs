using System;

namespace MediTechBackendAPI.Models
{
    public class MedicalRecord
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string RecordFor { get; set; } = string.Empty;
        public string Comments { get; set; } = string.Empty;
        public string FileUrl { get; set; } = string.Empty;
    }
}
