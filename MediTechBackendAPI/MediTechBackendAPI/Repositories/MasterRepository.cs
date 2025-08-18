using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MediTechBackendAPI.Models;

namespace MediTechBackendAPI.Repositories
{
	public class MasterRepository : IMasterRepository
	{
		private readonly IDbConnection _connection;

		public MasterRepository(IDbConnection connection)
		{
			_connection = connection;
		}

		public async Task<IEnumerable<Country>> GetCountriesAsync()
		{
			var countries = await _connection.QueryAsync<Country>(
				"[purojit2_emeditechppo].[USP_GetCountries]",
				commandType: CommandType.StoredProcedure);
			return countries;
		}

		public async Task<IEnumerable<State>> GetStatesByCountryAsync(int countryId)
		{
			var parameters = new DynamicParameters();
			parameters.Add("@CountryID", countryId, DbType.Int32);

			var states = await _connection.QueryAsync<State>(
				"[purojit2_emeditechppo].[USP_GetStatesByCountry]",
				parameters,
				commandType: CommandType.StoredProcedure);
			return states;
		}

		public async Task<IEnumerable<District>> GetDistrictsByStateAsync(int stateId)
		{
			var parameters = new DynamicParameters();
			parameters.Add("@StateID", stateId, DbType.Int32);

			var districts = await _connection.QueryAsync<District>(
				"[purojit2_emeditechppo].[USP_GetDistrictsByState]",
				parameters,
				commandType: CommandType.StoredProcedure);
			return districts;
		}

		public async Task<IEnumerable<City>> GetCitiesByDistrictAsync(int districtId)
		{
			var parameters = new DynamicParameters();
			parameters.Add("@DistrictID", districtId, DbType.Int32);

			var cities = await _connection.QueryAsync<City>(
				"[purojit2_emeditechppo].[USP_GetCitiesByDistrict]",
				parameters,
				commandType: CommandType.StoredProcedure);
			return cities;
		}
	}
}


