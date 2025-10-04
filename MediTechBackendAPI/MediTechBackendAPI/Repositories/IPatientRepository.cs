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
        Task<IEnumerable<MedicalRecord>> GetMedicalRecordsAsync(int patientId);
        Task<IEnumerable<Prescription>> GetPrescriptionsAsync(int patientId);
        Task<PatientDemographyDashboardDetails?> GetPatientDemographyDashboardDetailsAsync(System.Guid pid);
    Task<bool> InsertDependentAsync(Dependent dependent);
    Task<bool> InsertOrUpdateDependentsAsync(System.Guid pid, string patientId, string createdBy, IEnumerable<Dependent> dependents);
        Task<IEnumerable<Dependent>> GetDependentsAsync(System.Guid pid);
        Task<(bool Success, string Message)> UpdateDependentStatusAsync(System.Guid dependentId, string updatedBy, bool status);
        Task<(bool Success, string Message)> DeleteDependentAsync(System.Guid dependentId, string deletedBy);
    }
}
