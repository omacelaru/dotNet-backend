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
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [ValidateModel]
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
