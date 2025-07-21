using System;

namespace MediTechBackendAPI.Models
{
    public class OverallReport
    {
        public int PatientId { get; set; }
        public DateTime LastVisit { get; set; }
        public int HealthScore { get; set; }
        public string Summary { get; set; } = string.Empty;
    }
}
