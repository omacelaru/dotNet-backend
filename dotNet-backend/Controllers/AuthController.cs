using dotNet_backend.Data.Exceptions;
using dotNet_backend.Models.User.DTO;
using dotNet_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            try
            {
                return Ok(await _authService.RegisterUserAsync(registerDto));

            }
            catch (AuthorizationException)
            {
                return Unauthorized();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            try
            {
                return Ok(await _authService.LoginUserAsync(loginDto));

            }
            catch (AuthorizationException)
            {
                return Unauthorized();
            }
        }


        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(string refreshToken)
        {
            try
            {
                return Ok(await _authService.RefreshTokenAsync(refreshToken));

            }
            catch (BadRequestException err)
            {
                return BadRequest(new { error = err.Message });
            }
        }

        [Authorize]
        [HttpGet("test")]
        public async Task<IActionResult> AuthTest()
        {
            return Ok("Ok");
        }
    }
}
