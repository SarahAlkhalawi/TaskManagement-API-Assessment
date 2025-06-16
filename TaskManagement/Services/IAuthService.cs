using TaskManagement.DTOs;

namespace TaskManagement.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
        Task<UserDto> GetUserByIdAsync(int userId);
    }
}
