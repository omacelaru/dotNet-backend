using dotNet_backend.Models.User.DTO;
using dotNet_backend.Models.User;
using System.Security.Claims;

namespace dotNet_backend.Services.AuthService
{
    public interface IAuthService
    {
        Task<User> RegisterUserAsync(RegisterDto registerDto);
        Task<object> LoginUserAsync(LoginDto loginDto);
        Task<object> RefreshTokenAsync(string refreshToken);
    }
}