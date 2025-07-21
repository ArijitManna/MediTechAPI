using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediTechBackendAPI.Models;

namespace MediTechBackendAPI.Repositories
{
    public class DummyPatientRepository : IPatientRepository
    {
        public Task<Patient?> GetPatientAsync(int patientId)
        {
            var patient = new Patient
            {
                Id = patientId,
                Name = "Hendrita Hayes",
                Gender = "Female",
                Age = 32
            };
            return Task.FromResult<Patient?>(patient);
        }

        public Task<IEnumerable<HealthRecord>> GetHealthRecordsAsync(int patientId)
        {
            var list = new List<HealthRecord>
            {
                new HealthRecord
                {
                    Id = 1,
                    PatientId = patientId,
                    HeartRate = 140,
                    Temperature = 37.5m,
                    GlucoseLevel = 80m,
                    SpO2 = 96m,
                    BloodPressure = 100,
                    BMI = 20.1m,
                    CreatedAt = DateTime.UtcNow.AddDays(-1)
                }
            };
            return Task.FromResult<IEnumerable<HealthRecord>>(list);
        }

        public Task<OverallReport?> GetOverallReportAsync(int patientId)
        {
            var report = new OverallReport
            {
                PatientId = patientId,
                LastVisit = DateTime.UtcNow.AddDays(-10),
                HealthScore = 95,
                Summary = "Your health is 95% Normal"
            };
            return Task.FromResult<OverallReport?>(report);
        }

        public Task<IEnumerable<Notification>> GetNotificationsAsync(int patientId)
        {
            var list = new List<Notification>
            {
                new Notification
                {
                    Id = 1,
                    PatientId = patientId,
                    Message = "Booking Confirmed",
                    CreatedAt = DateTime.UtcNow
                },
                new Notification
                {
                    Id = 2,
                    PatientId = patientId,
                    Message = "You have a new review",
                    CreatedAt = DateTime.UtcNow.AddDays(-5)
                }
            };
            return Task.FromResult<IEnumerable<Notification>>(list);
        }

        public Task<IEnumerable<Appointment>> GetAppointmentsAsync(int patientId)
        {
            var list = new List<Appointment>
            {
                new Appointment
                {
                    Id = 1,
                    PatientId = patientId,
                    Doctor = "Dr. Edalin Hendry",
                    Date = DateTime.UtcNow.AddDays(1).Date.AddHours(16),
                    Type = "Video Call",
                    Status = "Upcoming"
                },
                new Appointment
                {
                    Id = 2,
                    PatientId = patientId,
                    Doctor = "Dr. Juliet Gabriel",
                    Date = DateTime.UtcNow.AddDays(-3).Date.AddHours(15),
                    Type = "Clinic Visit",
                    Status = "Completed"
                }
            };
            return Task.FromResult<IEnumerable<Appointment>>(list);
        }

        public Task<IEnumerable<Dependent>> GetDependantsAsync(int patientId)
        {
            var list = new List<Dependent>
            {
                new Dependent
                {
                    Id = 1,
                    PatientId = patientId,
                    Name = "Laura",
                    Relationship = "Mother",
                    Age = 58,
                    Gender = "Female"
                },
                new Dependent
                {
                    Id = 2,
                    PatientId = patientId,
                    Name = "Mathew",
                    Relationship = "Father",
                    Age = 59,
                    Gender = "Male"
                }
            };
            return Task.FromResult<IEnumerable<Dependent>>(list);
        }

        public Task<IEnumerable<MedicalRecord>> GetMedicalRecordsAsync(int patientId)
        {
            var list = new List<MedicalRecord>
            {
                new MedicalRecord
                {
                    Id = 1,
                    PatientId = patientId,
                    Name = "Dr. Peter Griffin",
                    Date = DateTime.UtcNow.AddDays(-2),
                    RecordFor = "Laboratory",
                    Comments = "Blood work normal",
                    FileUrl = "https://example.com/record1.pdf"
                }
            };
            return Task.FromResult<IEnumerable<MedicalRecord>>(list);
        }

        public Task<IEnumerable<Prescription>> GetPrescriptionsAsync(int patientId)
        {
            var list = new List<Prescription>
            {
                new Prescription
                {
                    Id = 1,
                    PatientId = patientId,
                    Name = "Ibuprofen",
                    CreatedDate = DateTime.UtcNow.AddDays(-7),
                    PrescribedBy = "Dr. Peter Griffin"
                }
            };
            return Task.FromResult<IEnumerable<Prescription>>(list);
        }
    }
}
