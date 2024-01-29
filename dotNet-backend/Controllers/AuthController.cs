using dotNet_backend.CustomActionFilters;
using dotNet_backend.Models.User.DTO;
using dotNet_backend.Services.AuthService;
using dotNet_backend.Services.SMTP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService): ControllerBase
    {
        private readonly IAuthService _authService =authService;
        
        [HttpPost("login")]
        [ValidateModel]
        public async Task<IActionResult> Login(LoginDto loginDto) =>
            await _authService.LoginUserAsync(loginDto);

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(string refreshToken) =>
            await _authService.RefreshTokenAsync(refreshToken);

        [Authorize]
        [HttpGet("test")]
        public IActionResult AuthTest() => Ok("Ok");
    }
}