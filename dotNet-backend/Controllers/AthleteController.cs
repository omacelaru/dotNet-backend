using dotNet_backend.Models.Athlete.DTO;
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
    }
}
