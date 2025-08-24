using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediTechBackendAPI.Models;
using MediTechBackendAPI.Services;

namespace MediTechBackendAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class EmailController : ControllerBase
	{
		private readonly IEmailService _emailService;

		public EmailController(IEmailService emailService)
		{
			_emailService = emailService;
		}

		[HttpPost("send")]
		public async Task<IActionResult> SendEmail([FromBody] EmailRequest emailRequest)
		{
			if (emailRequest == null)
			{
				return BadRequest(ApiResponse<bool>.Failure("Email request data is required"));
			}

			if (string.IsNullOrWhiteSpace(emailRequest.EmailId) || 
				string.IsNullOrWhiteSpace(emailRequest.Subject) || 
				string.IsNullOrWhiteSpace(emailRequest.Body))
			{
				return BadRequest(ApiResponse<bool>.Failure("EmailId, Subject, and Body are required"));
			}

			try
			{
				var result = await _emailService.SendEmailAsync(emailRequest);
				
				if (result)
				{
					return Ok(ApiResponse<bool>.Success(true, "Email sent successfully"));
				}
				else
				{
					return StatusCode(500, ApiResponse<bool>.Failure("Failed to send email"));
				}
			}
			catch (Exception)
			{
				return StatusCode(500, ApiResponse<bool>.Failure("An error occurred while sending email"));
			}
		}

		[HttpPost("send-otp")]
		public async Task<IActionResult> SendOtpEmail([FromBody] EmailRequest emailRequest)
		{
			if (emailRequest == null)
			{
				return BadRequest(ApiResponse<bool>.Failure("Email request data is required"));
			}

			if (string.IsNullOrWhiteSpace(emailRequest.EmailId))
			{
				return BadRequest(ApiResponse<bool>.Failure("EmailId is required"));
			}

			try
			{
				// Generate a 6-digit OTP
				var otp = new Random().Next(100000, 999999).ToString();
				var subject = string.IsNullOrWhiteSpace(emailRequest.Subject) ? "OTP Verification" : emailRequest.Subject;
				
				var result = await _emailService.SendOtpEmailAsync(emailRequest.EmailId, otp, subject);
				
				if (result)
				{
					return Ok(ApiResponse<bool>.Success(true, $"OTP sent successfully to {emailRequest.EmailId}"));
				}
				else
				{
					return StatusCode(500, ApiResponse<bool>.Failure("Failed to send OTP email"));
				}
			}
			catch (Exception)
			{
				return StatusCode(500, ApiResponse<bool>.Failure("An error occurred while sending OTP email"));
			}
		}
	}
}
