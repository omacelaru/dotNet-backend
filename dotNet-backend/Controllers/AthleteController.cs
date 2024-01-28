using dotNet_backend.Models.Athlete.DTO;
using dotNet_backend.Models.Request.DTO;
using dotNet_backend.Models.User.Enum;
using dotNet_backend.Services.AthleteService;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult<IEnumerable<AthleteResponseDto>>> GetAllAthletes() => 
            await _athleteService.GetAllAthletesAsync();
        
        [HttpGet("me")]
        [Authorize(Roles = "Athlete")]
        public async Task<ActionResult<AthleteResponseDto>> GetAthlete() => 
            await _athleteService.GetAthleteByUsernameAsync(User.Identity.Name);
        
        [HttpGet("me/requests")]
        [Authorize(Roles = "Athlete")]
        public async Task<ActionResult<IEnumerable<RequestInfoResponseDto>>> GetAthleteRequests() => 
            await _athleteService.GetAthleteRequestsAsync(User.Identity.Name);
        
        [HttpDelete("me/requests/{Id:Guid}")]
        [Authorize(Roles = "Athlete")]
        public async Task<ActionResult<RequestInfoResponseDto>> DeleteAthleteRequest(Guid Id) => 
            await _athleteService.DeleteAthleteRequestAsync(Id, User.Identity.Name);

        
        
    }
}
