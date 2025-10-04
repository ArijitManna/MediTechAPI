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

                var result = await _connection.QuerySingleAsync<dynamic>(
                    "[purojit2_emeditechppo].[USP_InsertOrUpdate_DependentFamilyMembers]",
                    new
                    {
                        PID = pid,
                        patientID = patientId,
                        Created_by = createdBy,
                        Dependents = dt.AsTableValuedParameter("[purojit2_emeditechppo].[UDT_DependentFamilyMembers]")
                    },
                    commandType: CommandType.StoredProcedure
                );

                Console.WriteLine(result.Message);

                // Now you know exactly what happened
                return (result.InsertedCount + result.UpdatedCount) > 0;
            }
            catch(Exception)
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

        public async Task<(bool Success, string Message)> UpdateDependentStatusAsync(System.Guid dependentId, string updatedBy, bool status)
        {
            try
            {
                var result = await _connection.QuerySingleAsync<dynamic>(
                    "[purojit2_emeditechppo].[USP_Update_DependentFamilyMemberStatus]",
                    new
                    {
                        DependentID = dependentId,
                        UpdatedBy = updatedBy,
                        Status = status
                    },
                    commandType: CommandType.StoredProcedure
                );

                bool success = result.RowsAffected > 0;
                return (success, result.Message);
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        public async Task<(bool Success, string Message)> DeleteDependentAsync(System.Guid dependentId, string deletedBy)
        {
            try
            {
                var result = await _connection.QuerySingleAsync<dynamic>(
                    "[purojit2_emeditechppo].[USP_Delete_DependentFamilyMember]",
                    new
                    {
                        DependentID = dependentId,
                        DeletedBy = deletedBy
                    },
                    commandType: CommandType.StoredProcedure
                );

                bool success = result.RowsAffected > 0;
                return (success, result.Message);
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }
    }
}
