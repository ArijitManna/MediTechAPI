using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace MediTechBackendAPI.Services
{
    public class LoginService : ILoginService
    {
        private readonly IDbConnection _connection;

        public LoginService(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<(string Otp, Guid Pid)> LoginWithOtpAsync(string emailId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EmailID", emailId);
            parameters.Add("@OTP_Code", dbType: DbType.String, size: 6, direction: ParameterDirection.Output);
            parameters.Add("@PID", dbType: DbType.Guid, direction: ParameterDirection.Output);

            await _connection.ExecuteAsync("purojit2_emeditechppo.usp_LoginWithOTP", parameters, commandType: CommandType.StoredProcedure);

            var otp = parameters.Get<string>("@OTP_Code");
            var pid = parameters.Get<Guid>("@PID");

            return (otp, pid);
        }

        public async Task<(bool IsValid, string Message)> ValidateOtpAsync(Guid pid, string otpCode)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PID", pid);
            parameters.Add("@OTP_Code", otpCode);

            var result = await _connection.QueryFirstOrDefaultAsync<(int IsValid, string Message)>(
                "purojit2_emeditechppo.usp_ValidateByOTP", parameters, commandType: CommandType.StoredProcedure);

            return (result.IsValid == 1, result.Message);
        }
    }
}
