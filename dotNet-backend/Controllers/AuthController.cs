using dotNet_backend.CustomActionFilters;
using dotNet_backend.Data.Exceptions;
using dotNet_backend.Models.User.DTO;
using dotNet_backend.Services;
using dotNet_backend.Services.AuthService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("login")]
        [ValidateModel]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            _logger.LogInformation("Logging in user {}", loginDto);
            return Ok(await _authService.LoginUserAsync(loginDto));
        }
        
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(string refreshToken)
        {
            _logger.LogInformation("Refreshing token {}", refreshToken);
            return Ok(await _authService.RefreshTokenAsync(refreshToken));
        }

        [Authorize]
        [HttpGet("test")]
        public async Task<IActionResult> AuthTest()
        {
            return Ok("Ok");
        }
    }
}
