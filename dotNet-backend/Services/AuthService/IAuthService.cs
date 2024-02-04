using dotNet_backend.Models.User.DTO;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_backend.Services.AuthService
{
    public interface IAuthService
    {
        Task<IActionResult> LoginUserAsync(LoginDto loginDto);
        Task<IActionResult> RefreshTokenAsync(string refreshToken);
        Task<IActionResult> VerifyEmailAsync(string token);
    }
}