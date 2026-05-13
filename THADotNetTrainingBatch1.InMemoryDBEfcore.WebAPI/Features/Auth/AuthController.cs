using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace THADotNetTrainingBatch1.InMemoryDBEfcore.WebAPI.Features.Auth;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(AuthRequest request)
    {
        var response = await _authService.LoginAsync(request);

        if (response is null)
            return Unauthorized(new { Message = "Invalid username or password." });

        return Ok(response);
    }
}
