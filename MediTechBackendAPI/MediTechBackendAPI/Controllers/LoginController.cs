using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediTechBackendAPI.Services;

namespace MediTechBackendAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly Helpers.JwtTokenHelper _jwtTokenHelper;

        public LoginController(ILoginService loginService, Helpers.JwtTokenHelper jwtTokenHelper)
        {
            _loginService = loginService;
            _jwtTokenHelper = jwtTokenHelper;
        }

        [HttpPost("otp")]
        public async Task<IActionResult> LoginWithOtp([FromBody] LoginOtpRequest request)
        {
            try
            {
                (string otp, System.Guid pid) = await _loginService.LoginWithOtpAsync(request.EmailID);
                return Ok(new { PID = pid, OTP = otp });
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                // Check for custom error from stored procedure
                if (ex.Message.Contains("Invalid EmailID"))
                {
                    return BadRequest(new { Error = "Email ID not found or invalid." });
                }
                throw;
            }
        }

        [HttpPost("validate-otp")]
        public async Task<IActionResult> ValidateOtp([FromBody] ValidateOtpRequest request)
        {
            try
            {
                var result = await _loginService.ValidateOtpAsync(request.PID, request.OTP_Code);
                if (result.IsValid)
                {
                    // For demo, using PID as email. Replace with actual email lookup if needed.
                    var token = _jwtTokenHelper.GenerateToken(request.PID, request.PID.ToString());
                    return Ok(new { Success = true, Message = result.Message, Token = token });
                }
                else
                {
                    return BadRequest(new { Success = false, Message = result.Message });
                }
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                return StatusCode(500, new { Success = false, Message = ex.Message });
            }
        }

        public class LoginOtpRequest
        {
            public string? EmailID { get; set; }
        }

        public class ValidateOtpRequest
        {
            public Guid PID { get; set; }
            public string OTP_Code { get; set; } = string.Empty;
        }
    }
}
