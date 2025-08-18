namespace MediTechBackendAPI.Models
{
	public class ApiResponse<T>
	{
		public int Status { get; set; }
		public T? Data { get; set; }
		public string Message { get; set; } = string.Empty;

		public static ApiResponse<T> Success(T data, string message = "Success")
		{
			return new ApiResponse<T>
			{
				Status = 1,
				Data = data,
				Message = message
			};
		}

		public static ApiResponse<T> Failure(string message)
		{
			return new ApiResponse<T>
			{
				Status = 0,
				Data = default,
				Message = message
			};
		}
	}
}


