using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
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
            // For backward compatibility, wrap single dependent in a list and call bulk method
            return await InsertOrUpdateDependentsAsync(
                dependent.PID,
                dependent.PatientID,
                dependent.Created_by,
                new List<Dependent> { dependent }
            );
        }

        public async Task<bool> InsertOrUpdateDependentsAsync(System.Guid pid, string patientId, string createdBy, IEnumerable<Dependent> dependents)
        {
            try
            {
                var dt = new System.Data.DataTable();
                dt.Columns.Add("Dependent_ID", typeof(System.Guid));
                dt.Columns.Add("First_Name", typeof(string));
                dt.Columns.Add("Middle_Name", typeof(string));
                dt.Columns.Add("Last_Name", typeof(string));
                dt.Columns.Add("Age", typeof(Int32));
                dt.Columns.Add("RelationshipID", typeof(int));

                foreach (var d in dependents)
                {
                    dt.Rows.Add(
                        d.Dependent_ID == null ? (object)DBNull.Value : d.Dependent_ID,
                        d.First_Name ?? string.Empty,
                        d.Middle_Name ?? string.Empty,
                        d.Last_Name ?? string.Empty,
                        d.Age,
                        d.RelationshipID
                    );
                }

                var parameters = new DynamicParameters();
                parameters.Add("@PID", pid);
                parameters.Add("@patientID", patientId);
                parameters.Add("@Created_by", createdBy);
                parameters.Add("@Dependents", dt.AsTableValuedParameter("[purojit2_emeditechppo].[UDT_DependentFamilyMembers]"));

                var rows = await _connection.ExecuteAsync(
                    "[purojit2_emeditechppo].[USP_InsertOrUpdate_DependentFamilyMembers]",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
                return rows > 0;
            }
            catch(Exception ex)
            {
                return false;
            }
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
