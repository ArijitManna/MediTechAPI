namespace MediTechBackendAPI.Models
{
	public class EmailRequest
	{
		public string EmailId { get; set; } = string.Empty;
		public string Subject { get; set; } = string.Empty;
		public string Body { get; set; } = string.Empty;
	}
}
