using dotNet_backend.Models.Athlete.DTO;
using dotNet_backend.Models.Request.DTO;
using dotNet_backend.Services.AthleteService;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AthleteController : ControllerBase
    {
        private readonly IAthleteService _athleteService;
        private readonly ILogger<AthleteController> _logger;

        public AthleteController(IAthleteService athleteService, ILogger<AthleteController> logger)
        {
            _athleteService = athleteService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AthleteResponseDto>>> GetAllAthletes()
        {
            _logger.LogInformation("Getting all athletes");
            return await _athleteService.GetAllAthletesAsync();
        }
        [HttpGet("me")]
        public async Task<ActionResult<AthleteResponseDto>> GetAthlete()
        {
            string username = User.Identity.Name;
            _logger.LogInformation("Getting athlete me {username}", username);
            return await _athleteService.GetAthleteByUsernameAsync(username);
        }
        
        [HttpGet("me/requests")]
        public async Task<ActionResult<IEnumerable<RequestInfoResponseDto>>> GetAthleteRequests()
        {
            string username = User.Identity.Name;
            _logger.LogInformation("Getting athlete [me {username}] requests", username);
            return await _athleteService.GetAthleteRequestsAsync(username);
        }
        
    }
}
