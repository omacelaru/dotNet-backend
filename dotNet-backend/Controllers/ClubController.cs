using AutoMapper;
using dotNet_backend.CustomActionFilters;
using dotNet_backend.Models.Club;
using dotNet_backend.Models.Club.DTO;
using dotNet_backend.Models.Coach;
using dotNet_backend.Services.ClubService;
using dotNet_backend.Services.CoachService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClubController : ControllerBase
    {
        public readonly IClubService _clubService;
        public readonly ICoachService _coachService;
        public readonly ILogger<ClubController> _logger;
        public readonly IMapper _mapper;

        public ClubController(IClubService clubService, ICoachService coachService, ILogger<ClubController> logger,
            IMapper mapper)
        {
            _clubService = clubService;
            _coachService = coachService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClubResponseDto>>> GetAllClubs()
        {
            _logger.LogInformation("Getting all clubs");
            var clubs = await _clubService.GetAllClubsAsync();
            var clubResponseDtos = _mapper.Map<IEnumerable<ClubResponseDto>>(clubs);
            return Ok(clubResponseDtos);
        }

        [HttpPost]
        [Authorize(Roles = "Coach")]
        [ValidateModel]
        public async Task<ActionResult<ClubResponseDto>> CreateClubFromCoach([FromBody] ClubRequestDto clubRequestDto)
        {
            _logger.LogInformation("Creating club {}", clubRequestDto);
            var club = _mapper.Map<Club>(clubRequestDto);
            var coach = await _coachService.GetCoachByUserNameAsync(User.Identity.Name);
            club.Coach = coach;
            var newClub = await _clubService.CreateClubAsync(club);
            var clubResponseDto = _mapper.Map<ClubResponseDto>(newClub);
            return Ok(clubResponseDto);
        }
    }
}