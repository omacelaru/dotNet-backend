using dotNet_backend.Models.User.DTO;
using dotNet_backend.Models.User;

namespace dotNet_backend.Services.AuthService
{
    public interface IAuthService
    {
        Task<User> RegisterUserAsync(RegisterDto registerDto);
        string GenerateJwtToken(User user);
        Task<string> LoginUserAsync(LoginDto loginDto);
        bool VerifyPassword(string hashedPassword, string providedPassword);
    }
}