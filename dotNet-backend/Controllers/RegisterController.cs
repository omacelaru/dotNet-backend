using dotNet_backend.CustomActionFilters;
using dotNet_backend.Models.Athlete.DTO;
using dotNet_backend.Models.Coach.DTO;
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
        public async Task<ActionResult<AthleteResponseDto>> RegisterAthlete([FromBody] AthleteRegisterDto athleteRegisterDto) =>
            await _registerService.RegisterAthleteAsync(athleteRegisterDto);

        [HttpPost("coach")]
        [ValidateModel]
        public async Task<ActionResult<CoachResponseDto>> RegisterCoach([FromBody] CoachRegisterDto coachRegisterDto) =>
            await _registerService.RegisterCoachAsync(coachRegisterDto);
    }
}
