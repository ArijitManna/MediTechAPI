using System;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MediTechBackendAPI.Models;

namespace MediTechBackendAPI.Services
{
	public class EmailService : IEmailService
	{
		private readonly EmailSettings _emailSettings;

		public EmailService(IOptions<EmailSettings> emailSettings)
		{
			_emailSettings = emailSettings.Value;
		}

		public async Task<bool> SendEmailAsync(EmailRequest emailRequest)
		{
			try
			{
				using var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort)
				{
					EnableSsl = _emailSettings.EnableSsl,
					UseDefaultCredentials = false,
					Credentials = new System.Net.NetworkCredential(_emailSettings.Username, _emailSettings.Password),
					DeliveryMethod = SmtpDeliveryMethod.Network
				};

				var mailMessage = new MailMessage
				{
					From = new MailAddress(_emailSettings.FromEmail, _emailSettings.FromName),
					Subject = emailRequest.Subject,
					Body = emailRequest.Body,
					IsBodyHtml = true
				};

				mailMessage.To.Add(emailRequest.EmailId);

				await client.SendMailAsync(mailMessage);
				return true;
			}
			catch (Exception ex)
			{
				// Log the exception here if you have logging configured
				// _logger.LogError(ex, "Error sending email to {EmailId}: {Message}", emailRequest.EmailId, ex.Message);
				Console.WriteLine($"Email error: {ex.Message}");
				return false;
			}
		}

		public async Task<bool> SendOtpEmailAsync(string emailId, string otp, string subject = "OTP Verification")
		{
			var body = $@"
				<html>
				<body>
					<h2>OTP Verification</h2>
					<p>Your OTP for verification is: <strong>{otp}</strong></p>
					<p>This OTP is valid for 100 minutes.</p>
					<p>If you didn't request this OTP, please ignore this email.</p>
					<br/>
					<p>Best regards,<br/>MediTech Plus Team</p>
				</body>
				</html>";

			var emailRequest = new EmailRequest
			{
				EmailId = emailId,
				Subject = subject,
				Body = body
			};

			return await SendEmailAsync(emailRequest);
		}
	}
}
