using dotNet_backend.Models.Participation.DTO;
using dotNet_backend.Services.ParticipationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ParticipationController(IParticipationService participationService) : ControllerBase
    {
        private readonly IParticipationService _participationService = participationService;
        
        [HttpPost("competition/{competitionId:guid}")]
        [Authorize(Roles = "Coach")]
        public async Task<ActionResult<ParticipationResponseDto>> AddAthletesToCompetition(Guid competitionId, [FromBody] ParticipationRequestDto participationRequestDto) =>
            await _participationService.AddAthletesToCompetitionAsync(competitionId, participationRequestDto, User.Identity.Name);
        
    }
}