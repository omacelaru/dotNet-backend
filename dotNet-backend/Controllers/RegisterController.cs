using dotNet_backend.CustomActionFilters;
using dotNet_backend.Data.Exceptions;
using dotNet_backend.Models.Athlete.DTO;
using dotNet_backend.Models.Coach.DTO;
using dotNet_backend.Services.RegisterService;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterService _registerService;
        private readonly ILogger<RegisterController> _logger;
        public RegisterController(IRegisterService registerService, ILogger<RegisterController> logger) 
        {
            _registerService = registerService;
            _logger = logger;
        }
        [HttpPost("athlete")]
        [ValidateModel]
        public async Task<IActionResult> RegisterAthlete([FromBody] AthleteRegisterDto athleteRegisterDto)
        {
            _logger.LogInformation("Registering athlete {}", athleteRegisterDto);
            return Ok(await _registerService.RegisterAthleteAsync(athleteRegisterDto));
        }

        [HttpPost("coach")]
        [ValidateModel]
        public async Task<IActionResult> RegisterCoach([FromBody] CoachRegisterDto coachRegisterDto)
        {
            _logger.LogInformation("Registering coach {}", coachRegisterDto);
            return Ok(await _registerService.RegisterCoachAsync(coachRegisterDto));
        }   

    }
}
