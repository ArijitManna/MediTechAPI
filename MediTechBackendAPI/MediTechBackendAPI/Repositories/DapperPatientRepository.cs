using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MediTechBackendAPI.Models;

namespace MediTechBackendAPI.Repositories
{
    public class DapperPatientRepository : IPatientRepository
    {
        private readonly IDbConnection _connection;

        public DapperPatientRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        // TODO: Replace the following methods with actual stored procedure calls
        public Task<Patient?> GetPatientAsync(int patientId)
        {
            // Example:
            // return _connection.QueryFirstOrDefaultAsync<Patient>("sp_GetPatient", new { patientId }, commandType: CommandType.StoredProcedure);
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<HealthRecord>> GetHealthRecordsAsync(int patientId)
        {
            throw new System.NotImplementedException();
        }

        public Task<OverallReport?> GetOverallReportAsync(int patientId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Notification>> GetNotificationsAsync(int patientId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Appointment>> GetAppointmentsAsync(int patientId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Dependent>> GetDependantsAsync(int patientId)
        {
            throw new System.NotImplementedException();
        }
    }
}
