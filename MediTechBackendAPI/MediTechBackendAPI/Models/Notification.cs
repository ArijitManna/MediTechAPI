using System;

namespace MediTechBackendAPI.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
