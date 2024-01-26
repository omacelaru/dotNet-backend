using AutoMapper;
using dotNet_backend.Models.Request;
using dotNet_backend.Models.Request.DTO;
using dotNet_backend.Models.Request.Enum;
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
        private readonly IClubService _clubService;
        private readonly ICoachService _coachService;
        private readonly IAthleteService _athleteService;
        private readonly IRequestService _requestService;
        private readonly IMapper _mapper;

        public RequestController(IClubService clubService, ICoachService coachService, IAthleteService athleteService,
            IRequestService requestService, IMapper mapper)
        {
            _clubService = clubService;
            _coachService = coachService;
            _athleteService = athleteService;
            _requestService = requestService;
            _mapper = mapper;
        }

        //make request to join in the athlete list of a coach by coach username
        [HttpPost("join/{coachUsername}")]
        [Authorize(Roles = "Athlete")]
        public async Task<ActionResult<RequestInfoResponseDto>> JoinCoach(string coachUsername)
        {
            string athleteUsername = User.Identity.Name;
            return await _requestService.CreateRequestAsync(athleteUsername, coachUsername, RequestType.AddAthleteToCoach);
        }
    }
}