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

        [HttpPost("athlete")]
        [ValidateModel]
        public async Task<ActionResult<AthleteResponseDto>> RegisterAthlete([FromBody] RegisterDto athleteRegisterDto) =>
            await _registerService.RegisterAthleteAsync(athleteRegisterDto);

        [HttpPost("coach")]
        [ValidateModel]
        public async Task<ActionResult<CoachResponseDto>> RegisterCoach([FromBody] RegisterDto coachRegisterDto) =>
            await _registerService.RegisterCoachAsync(coachRegisterDto);
        
        [HttpPost("admin")]
        [ValidateModel]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterDto adminRegisterDto) =>
            await _registerService.RegisterAdminAsync(adminRegisterDto);
    }
}
