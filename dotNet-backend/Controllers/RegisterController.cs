using dotNet_backend.CustomActionFilters;
using dotNet_backend.Models.Athlete.DTO;
using dotNet_backend.Models.Coach.DTO;
using dotNet_backend.Models.User.DTO;
using dotNet_backend.Services.RegisterService;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegisterController(IRegisterService registerService) : ControllerBase
    {
        private readonly IRegisterService _registerService = registerService;

        /// <summary>
        /// Register an athlete
        /// </summary>
        /// <param name="athleteRegisterDto"></param>
        [HttpPost("athlete")]
        [ValidateModel]
        public async Task<ActionResult<AthleteCoachNameResponseDto>> RegisterAthlete([FromBody] RegisterDto athleteRegisterDto) =>
            await _registerService.RegisterAthleteAsync(athleteRegisterDto);

        /// <summary>
        /// Register a coach
        /// </summary>
        /// <param name="coachRegisterDto"></param>
        [HttpPost("coach")]
        [ValidateModel]
        public async Task<ActionResult<CoachResponseDto>> RegisterCoach([FromBody] RegisterDto coachRegisterDto) =>
            await _registerService.RegisterCoachAsync(coachRegisterDto);

        /// <summary>
        /// Register an admin
        /// </summary>
        /// <param name="adminRegisterDto"></param>
        [HttpPost("admin")]
        ///[NonAction] // Comment this line to enable admin registration
        [ValidateModel]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterDto adminRegisterDto) =>
            await _registerService.RegisterAdminAsync(adminRegisterDto);

        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<RegisterResponseDto>> Register([FromBody] RegisterDto registerDto) =>
            await _registerService.RegisterUser(registerDto);
    }
}
