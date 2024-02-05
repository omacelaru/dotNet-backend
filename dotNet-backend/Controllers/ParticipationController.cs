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
        
        /// <summary>
        /// Add the awards of an athlete in a competition
        /// </summary>
        [HttpPatch("competition/{competitionId:guid}/athlete/{athleteUsername}")]
        [Authorize(Roles = "Coach")] 
        public async Task<ActionResult<ParticipationAthleteWithAwardsResponseDto>> UpdateAthleteParticipationWithAwards(Guid competitionId, string athleteUsername, [FromBody] ParticipationAwardsRequestDto participationAwardsRequestDto) =>
            await _participationService.UpdateAthleteParticipationWithAwardsAsync(competitionId, athleteUsername, participationAwardsRequestDto, User.Identity.Name);
    }
}