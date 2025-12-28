using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using MediTechBackendAPI.Models;

namespace MediTechBackendAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VitalsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public VitalsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET api/vitals or GET api/vitals/{id}
        [HttpGet("{id?}")]
        public async Task<IActionResult> Get(Guid? id = null)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            using var connection = new SqlConnection(connectionString);

            var parameters = new DynamicParameters();
            parameters.Add("@Id", id, DbType.Guid);

            var results = await connection.QueryAsync("usp_GetOrListPatientVital", parameters, commandType: CommandType.StoredProcedure);

            if (id.HasValue)
            {
                var single = results.AsList().Count > 0 ? results.AsList()[0] : null;
                return Ok(single == null ? 
                    ApiResponse<object>.Failure("Not found") : 
                    ApiResponse<object>.Success(single, "Record fetched"));
            }

            return Ok(ApiResponse<object>.Success(results, "List fetched"));
        }

        // POST api/vitals  -> Add or Update via existing upsert proc
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PatientVitalDto dto)
        {
            if (dto == null)
                return BadRequest(ApiResponse<object>.Failure("Invalid payload"));

            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            using var connection = new SqlConnection(connectionString);

            var parameters = new DynamicParameters();
            parameters.Add("@Id", dto.Id == Guid.Empty ? (Guid?)null : dto.Id, DbType.Guid);
            parameters.Add("@PatientId", dto.PatientId, DbType.Guid);
            parameters.Add("@BMI", dto.BMI, DbType.Decimal);
            parameters.Add("@HeartRate", dto.HeartRate, DbType.Int32);
            parameters.Add("@Weight", dto.Weight, DbType.Decimal);
            parameters.Add("@FBC", dto.FBC, DbType.String);
            parameters.Add("@Glucose", dto.Glucose, DbType.Decimal);
            parameters.Add("@Temperature", dto.Temperature, DbType.Decimal);
            parameters.Add("@SpO2", dto.SpO2, DbType.Int32);
            parameters.Add("@Systolic", dto.Systolic, DbType.Int32);
            parameters.Add("@Diastolic", dto.Diastolic, DbType.Int32);
            parameters.Add("@RecordedOn", dto.RecordedOn ?? DateTime.UtcNow, DbType.DateTime);

            // calling existing upsert stored procedure
            await connection.ExecuteAsync("usp_UpsertPatientVital_GUID", parameters, commandType: CommandType.StoredProcedure);

            return Ok(ApiResponse<object>.Success(new { }, "Vitals added/updated successfully"));
        }
    }
}
