using System;

namespace MediTechBackendAPI.Models
{
	public class City
	{
		public int CityID { get; set; }
		public string CityName { get; set; } = string.Empty;
		public string Pincode { get; set; } = string.Empty;
	}
}


