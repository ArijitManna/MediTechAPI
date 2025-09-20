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
        public async Task<bool> InsertDependentAsync(Dependent dependent)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PID", dependent.PID);
            parameters.Add("@PatientID", dependent.PatientID);
            parameters.Add("@First_Name", dependent.First_Name);
            parameters.Add("@Middle_Name", dependent.Middle_Name);
            parameters.Add("@Last_Name", dependent.Last_Name);
            parameters.Add("@Age", dependent.Age);
            parameters.Add("@RelationshipID", dependent.RelationshipID);
            parameters.Add("@Created_by", dependent.Created_by);

            var rows = await _connection.ExecuteAsync(
                "[purojit2_emeditechppo].[USP_Insert_DependentFamilyMember]",
                parameters,
                commandType: CommandType.StoredProcedure
            );
            return rows > 0;
        }

        public async Task<IEnumerable<Dependent>> GetDependentsAsync(System.Guid pid)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PID", pid);

            var dependents = await _connection.QueryAsync<Dependent>(
                "[purojit2_emeditechppo].[USP_List_DependentFamilyMembers]",
                parameters,
                commandType: CommandType.StoredProcedure
            );
            return dependents;
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

        public Task<IEnumerable<MedicalRecord>> GetMedicalRecordsAsync(int patientId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Prescription>> GetPrescriptionsAsync(int patientId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Models.PatientDemographyDashboardDetails?> GetPatientDemographyDashboardDetailsAsync(System.Guid pid)
        {
            var parameters = new { PID = pid };
            var result = await _connection.QueryFirstOrDefaultAsync<Models.PatientDemographyDashboardDetails>(
                "purojit2_emeditechppo.GetPatientDemographyDashboardDetails",
                parameters,
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
    }
}
