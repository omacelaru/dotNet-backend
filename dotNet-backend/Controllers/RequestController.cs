using dotNet_backend.Services.AthleteService;
using dotNet_backend.Services.ClubService;
using dotNet_backend.Services.CoachService;
using dotNet_backend.Services.RequestService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        public readonly IClubService _clubService;
        public readonly ICoachService _coachService;
        public readonly IAthleteService _athleteService;
        public readonly IRequestService _requestService;
        
        public RequestController(IClubService clubService, ICoachService coachService, IAthleteService athleteService, IRequestService requestService)  
        {
            _clubService = clubService;
            _coachService = coachService;
            _athleteService = athleteService;
            _requestService = requestService;
        }

        //make request to join in the athlete list of a coach by coach username
        [HttpPost("join/{coachUsername}")]
        [Authorize(Roles = "Athlete")]
        public async Task<IActionResult> JoinCoach(string coachUsername)
        {
            try
            {
                var coach = await _coachService.GetCoachByUserNameAsync(coachUsername);
                var athlete = await _athleteService.GetAthleteByUserNameAsync(User.Identity.Name);
                await _requestService.JoinCoachAsync(coach, athlete);
                //TODO - return something:(
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
