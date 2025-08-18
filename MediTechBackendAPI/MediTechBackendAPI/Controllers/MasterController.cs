using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediTechBackendAPI.Models;
using MediTechBackendAPI.Repositories;

namespace MediTechBackendAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class MasterController : ControllerBase
	{
		private readonly IMasterRepository _repository;

		public MasterController(IMasterRepository repository)
		{
			_repository = repository;
		}


		[HttpGet("countries")]
		public async Task<IActionResult> GetCountries()
		{
			try
			{
				var countries = await _repository.GetCountriesAsync();
				var message = countries.Any() ? "Countries fetched successfully" : "No countries found";
				return Ok(ApiResponse<IEnumerable<Country>>.Success(countries, message));
			}
			catch (System.Exception ex)
			{
				return StatusCode(500, ApiResponse<IEnumerable<Country>>.Failure("An unexpected error occurred"));
			}
		}

		[HttpGet("states/{countryId:int}")]
		public async Task<IActionResult> GetStatesByCountry(int countryId)
		{
			if (countryId <= 0)
			{
				return BadRequest(ApiResponse<IEnumerable<State>>.Failure("Invalid countryId"));
			}

			try
			{
				var states = await _repository.GetStatesByCountryAsync(countryId);
				var message = states.Any() ? "States fetched successfully" : "No states found";
				return Ok(ApiResponse<IEnumerable<State>>.Success(states, message));
			}
			catch (System.Exception)
			{
				return StatusCode(500, ApiResponse<IEnumerable<State>>.Failure("An unexpected error occurred"));
			}
		}

		[HttpGet("districts/{stateId:int}")]
		public async Task<IActionResult> GetDistrictsByState(int stateId)
		{
			if (stateId <= 0)
			{
				return BadRequest(ApiResponse<IEnumerable<District>>.Failure("Invalid stateId"));
			}

			try
			{
				var districts = await _repository.GetDistrictsByStateAsync(stateId);
				var message = districts.Any() ? "Districts fetched successfully" : "No districts found";
				return Ok(ApiResponse<IEnumerable<District>>.Success(districts, message));
			}
			catch (System.Exception)
			{
				return StatusCode(500, ApiResponse<IEnumerable<District>>.Failure("An unexpected error occurred"));
			}
		}

		[HttpGet("cities/{districtId:int}")]
		public async Task<IActionResult> GetCitiesByDistrict(int districtId)
		{
			if (districtId <= 0)
			{
				return BadRequest(ApiResponse<IEnumerable<City>>.Failure("Invalid districtId"));
			}

			try
			{
				var cities = await _repository.GetCitiesByDistrictAsync(districtId);
				var message = cities.Any() ? "Cities fetched successfully" : "No cities found";
				return Ok(ApiResponse<IEnumerable<City>>.Success(cities, message));
			}
			catch (System.Exception)
			{
				return StatusCode(500, ApiResponse<IEnumerable<City>>.Failure("An unexpected error occurred"));
			}
		}
	}
}


