using Invoice_Manager_API.DTO.AuthDTO;

namespace Invoice_Manager_API.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterRequest registerRequest);
    Task<AuthResponseDto> LoginAsync(LoginRequest loginRequest);

}
