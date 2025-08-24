using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MediTechBackendAPI.Models;

namespace MediTechBackendAPI.Repositories
{
	public class PatientRegistrationRepository : IPatientRegistrationRepository
	{
		private readonly IDbConnection _connection;

		public PatientRegistrationRepository(IDbConnection connection)
		{
			_connection = connection;
		}

		public async Task<PatientRegistrationResponse> RegisterPatientAsync(PatientRegistration registration)
		{
			try
			{
				var parameters = new DynamicParameters();
				parameters.Add("@CountryCode", registration.CountryCode, DbType.String);
				parameters.Add("@Mobilenumber", registration.MobileNumber, DbType.String);
				parameters.Add("@EmailID", registration.EmailID, DbType.String);
				parameters.Add("@First_Name", registration.FirstName, DbType.String);
				parameters.Add("@Middle_Name", registration.MiddleName, DbType.String);
				parameters.Add("@Last_Name", registration.LastName, DbType.String);
				parameters.Add("@Age", registration.Age, DbType.Int32);
				parameters.Add("@Dob", registration.Dob, DbType.Date);
				parameters.Add("@Gender", registration.Gender, DbType.String);
				parameters.Add("@CountryID", registration.CountryID, DbType.Int32);
				parameters.Add("@StateID", registration.StateID, DbType.Int32);
				parameters.Add("@DistrictID", registration.DistrictID, DbType.Int32);
				parameters.Add("@CityID", registration.CityID, DbType.Int32);
				parameters.Add("@Created_by", registration.CreatedBy, DbType.String);
				parameters.Add("@GeneratedPID", dbType: DbType.Guid, direction: ParameterDirection.Output);
				parameters.Add("@GeneratedOTP", dbType: DbType.String, size: 6, direction: ParameterDirection.Output);
				parameters.Add("@generatedotpValidTime", dbType: DbType.String, size: 3, direction: ParameterDirection.Output);

				await _connection.ExecuteAsync(
					"[purojit2_emeditechppo].[USP_Insert_Patient_Registration]",
					parameters,
					commandType: CommandType.StoredProcedure);

				var generatedPID = parameters.Get<Guid>("@GeneratedPID");
				var generatedOTP = parameters.Get<string>("@GeneratedOTP") ?? string.Empty;
				var otpValidTime = parameters.Get<string>("@generatedotpValidTime") ?? string.Empty;

				if (generatedPID == Guid.Empty)
				{
					throw new InvalidOperationException("Failed to generate PID from stored procedure");
				}

				return new PatientRegistrationResponse
				{
					GeneratedPID = generatedPID,
					GeneratedOTP = generatedOTP,
					OtpValidTime = otpValidTime
				};
			}
			catch (Exception ex)
			{
				// Log the exception details here if you have logging configured
				// _logger.LogError(ex, "Error occurred during patient registration");
				
				throw new InvalidOperationException($"Patient registration failed: {ex.Message}", ex);
			}
		}

		public async Task<string> ValidateOtpAsync(Guid pid, string otp)
		{
			var parameters = new DynamicParameters();
			parameters.Add("@PID", pid, DbType.Guid);
			parameters.Add("@OTP", otp, DbType.String);
			parameters.Add("@Message", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);

			await _connection.ExecuteAsync(
				"[purojit2_emeditechppo].[USP_Validate_OTP_And_Create_Login]",
				parameters,
				commandType: CommandType.StoredProcedure);

			return parameters.Get<string>("@Message") ?? "Unknown error occurred";
		}
	}
}
