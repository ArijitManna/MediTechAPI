using System.Collections.Generic;
using System.Threading.Tasks;
using MediTechBackendAPI.Models;

namespace MediTechBackendAPI.Repositories
{
    public interface IPatientRepository
    {
        Task<Patient?> GetPatientAsync(int patientId);
        Task<IEnumerable<HealthRecord>> GetHealthRecordsAsync(int patientId);
        Task<OverallReport?> GetOverallReportAsync(int patientId);
        Task<IEnumerable<Notification>> GetNotificationsAsync(int patientId);
        Task<IEnumerable<Appointment>> GetAppointmentsAsync(int patientId);
        Task<IEnumerable<Dependent>> GetDependantsAsync(int patientId);
    }
}
