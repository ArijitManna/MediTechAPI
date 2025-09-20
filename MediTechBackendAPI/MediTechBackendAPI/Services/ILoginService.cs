using System.Threading.Tasks;
using MediTechBackendAPI.Models;

namespace MediTechBackendAPI.Services
{
    public interface ILoginService
    {
        Task<(string Otp, System.Guid Pid)> LoginWithOtpAsync(string emailId);
        Task<(bool IsValid, string Message)> ValidateOtpAsync(Guid pid, string otpCode);
    }
}
