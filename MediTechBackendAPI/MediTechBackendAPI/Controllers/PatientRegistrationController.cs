using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediTechBackendAPI.Models;
using MediTechBackendAPI.Repositories;

namespace MediTechBackendAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PatientRegistrationController : ControllerBase
	{
		private readonly IPatientRegistrationRepository _repository;

		public PatientRegistrationController(IPatientRegistrationRepository repository)
		{
			_repository = repository;
		}

		[HttpPost("register")]
		public async Task<IActionResult> RegisterPatient([FromBody] PatientRegistration registration)
		{
			if (registration == null)
			{
				return BadRequest(ApiResponse<PatientRegistrationResponse>.Failure("Registration data is required"));
			}

			if (string.IsNullOrWhiteSpace(registration.CountryCode) || string.IsNullOrWhiteSpace(registration.MobileNumber))
			{
				return BadRequest(ApiResponse<PatientRegistrationResponse>.Failure("CountryCode and MobileNumber are required"));
			}

			try
			{
				var response = await _repository.RegisterPatientAsync(registration);
				return Ok(ApiResponse<PatientRegistrationResponse>.Success(response, "Patient registered successfully. Please check your OTP."));
			}
			catch (Exception ex)
			{
				return StatusCode(500, ApiResponse<PatientRegistrationResponse>.Failure("Registration failed. Please try again."));
			}
		}

		[HttpPost("validate-otp")]
		public async Task<IActionResult> ValidateOtp([FromBody] OtpValidation otpValidation)
		{
			if (otpValidation == null)
			{
				return BadRequest(ApiResponse<string>.Failure("OTP validation data is required"));
			}

			if (otpValidation.PID == Guid.Empty || string.IsNullOrWhiteSpace(otpValidation.OTP))
			{
				return BadRequest(ApiResponse<string>.Failure("PID and OTP are required"));
			}

			try
			{
				var message = await _repository.ValidateOtpAsync(otpValidation.PID, otpValidation.OTP);
				
				if (message.Contains("Success", StringComparison.OrdinalIgnoreCase))
				{
					return Ok(ApiResponse<string>.Success(message, "OTP validated successfully"));
				}
				else
				{
					return BadRequest(ApiResponse<string>.Failure(message));
				}
			}
			catch (Exception)
			{
				return StatusCode(500, ApiResponse<string>.Failure("OTP validation failed. Please try again."));
			}
		}
	}
}
