using Invoice_Manager_API.DTO.AuthDTO;
using Invoice_Manager_API.Services.Interfaces;

namespace Invoice_Manager_API.Services;

public class AuthService : IAuthService
{
    public Task<AuthResponseDto> LoginAsync(LoginRequest loginRequest)
    {
        throw new NotImplementedException();
    }

    public Task<AuthResponseDto> RegisterAsync(RegisterRequest registerRequest)
    {
        throw new NotImplementedException();
    }
}
