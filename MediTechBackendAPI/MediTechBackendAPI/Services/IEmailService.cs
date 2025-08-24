using System.Threading.Tasks;
using MediTechBackendAPI.Models;

namespace MediTechBackendAPI.Services
{
	public interface IEmailService
	{
		Task<bool> SendEmailAsync(EmailRequest emailRequest);
		Task<bool> SendOtpEmailAsync(string emailId, string otp, string subject = "OTP Verification");
	}
}
