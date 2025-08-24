using System;
using System.Threading.Tasks;
using MediTechBackendAPI.Models;

namespace MediTechBackendAPI.Repositories
{
	public interface IPatientRegistrationRepository
	{
		Task<PatientRegistrationResponse> RegisterPatientAsync(PatientRegistration registration);
		Task<string> ValidateOtpAsync(Guid pid, string otp);
	}
}
