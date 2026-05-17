using Microsoft.AspNetCore.Mvc;
using THADotNetTrainingBatch1.Logging.WebAPI.Services.Auth;

namespace THADotNetTrainingBatch1.Logging.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logService;

    public AuthController(IAuthService authService, ILogger<AuthController> logService)
    {
        _authService = authService;
        _logService = logService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestModel request)
    {
        _logService.LogInformation($"Login attempt for user: {request.Username}");
        var response = await _authService.LoginAsync(request);
        if (response == null)
        {
            _logService.LogWarning($"Login failed for user: {request.Username}");
            return Unauthorized(new { message = "Invalid username or password" });
        }

        _logService.LogInformation($"Login successful for user: {request.Username}");
        return Ok(response);
    }

}
