using dotNet_backend.CustomActionFilters;
using dotNet_backend.Data.Exceptions;
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
        
        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="loginDto"></param>
        /// <response code="200">User logged in</response>
        /// <response code="401">Invalid credentials</response>
        /// <response code="403">User is not confirmed</response>
        [HttpPost("login")]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorMessage))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorMessage))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorMessage))]
        public async Task<IActionResult> Login(LoginDto loginDto) =>
            await _authService.LoginUserAsync(loginDto);

        /// <summary>
        /// Refresh token
        /// </summary>
        /// <param name="refreshToken"></param>
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(string refreshToken) =>
            await _authService.RefreshTokenAsync(refreshToken);

        [Authorize]
        [HttpGet("test")]
        public IActionResult AuthTest() => Ok("Ok");
    }
}