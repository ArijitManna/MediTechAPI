using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace MediTechBackendAPI.Middleware
{
    public class ApiSecretKeyMiddleware
    {
    private readonly RequestDelegate _next;
    private readonly string _apiSecretKey;
    private readonly string[] _allowedApiPaths;

        public ApiSecretKeyMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _apiSecretKey = configuration["ApiSecretKey"] ?? string.Empty;
            _allowedApiPaths = configuration.GetSection("AllowedApiPaths").Get<string[]>() ?? new string[0];
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (_allowedApiPaths.Any(p => context.Request.Path.StartsWithSegments(p)))
            {
                if (!context.Request.Headers.TryGetValue("x-api-secretkey", out var extractedKey) || extractedKey != _apiSecretKey)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Invalid or missing API secret key.");
                    return;
                }
            }
            await _next(context);
        }
    }
}
