using Invoice_Manager_API.Common;
using Invoice_Manager_API.DTO.AuthDTO;
using Invoice_Manager_API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Invoice_Manager_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        this._authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<ApiResponse<AuthResponseDto>>> 
        Register([FromBody] RegisterRequest request)
    {
        var result = await this._authService.RegisterAsync(request);

        return Ok(
                ApiResponse<AuthResponseDto>
                    .SuccessResponse(result, "User registered successfully")
            );
    }

    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<AuthResponseDto>>> 
        Login([FromBody] LoginRequest request)
    {
        var result = await this._authService.LoginAsync(request);

        return Ok(
                ApiResponse<AuthResponseDto>
                    .SuccessResponse(result, "User registered successfully")
                );
    }
}
