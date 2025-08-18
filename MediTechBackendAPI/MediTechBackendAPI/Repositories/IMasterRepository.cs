using System.Collections.Generic;
using System.Threading.Tasks;
using MediTechBackendAPI.Models;

namespace MediTechBackendAPI.Repositories
{
	public interface IMasterRepository
	{
		Task<IEnumerable<Country>> GetCountriesAsync();
		Task<IEnumerable<State>> GetStatesByCountryAsync(int countryId);
		Task<IEnumerable<District>> GetDistrictsByStateAsync(int stateId);
		Task<IEnumerable<City>> GetCitiesByDistrictAsync(int districtId);
	}
}


