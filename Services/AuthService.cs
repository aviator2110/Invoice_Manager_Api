using Invoice_Manager_API.DTO.AuthDTO;
using Invoice_Manager_API.Models;
using Invoice_Manager_API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using LoginRequest = Invoice_Manager_API.DTO.AuthDTO.LoginRequest;
using RegisterRequest = Invoice_Manager_API.DTO.AuthDTO.RegisterRequest;

namespace Invoice_Manager_API.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    public AuthService(UserManager<ApplicationUser> userManager)
    {
        this._userManager = userManager;
    }
    public async Task<AuthResponseDto> LoginAsync(LoginRequest loginRequest)
    {
        var user = await this._userManager
            .FindByEmailAsync(loginRequest.Email);

        if (user is null)
        {
            throw new UnauthorizedAccessException("Invalid name or password");
        }

        var isValidPassword = await this._userManager
            .CheckPasswordAsync(user, loginRequest.Password);

        if (!isValidPassword)
        {
            throw new UnauthorizedAccessException("Invalid name or password");
        }

        return new AuthResponseDto
        {
            Email = user.Email!
        };
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterRequest registerRequest)
    {
        var exsitingUser = await this._userManager
            .FindByEmailAsync(registerRequest.Email);

        if (exsitingUser is not null)
        {
            throw new InvalidOperationException
                ("User with this email already exists");
        }

        var user = new ApplicationUser
        {
            UserName = registerRequest.Email,
            Email = registerRequest.Email,
            FirstName = registerRequest.FirstName,
            LastName = registerRequest.LastName,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null
        };

        var result = await this._userManager
            .CreateAsync(user, registerRequest.Password);

        if (!result.Succeeded)
        {
            var errors = string
                .Join(", ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"User creation failed: {errors}");
        }

        return new AuthResponseDto
        {
            Email = user.Email
        };
    }
}
