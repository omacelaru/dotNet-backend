using dotNet_backend.Data.Exceptions;
using dotNet_backend.Models.User.DTO;
using dotNet_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_backend.Controllers
{
    [Route("api/[controller]")]
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
    }
}
