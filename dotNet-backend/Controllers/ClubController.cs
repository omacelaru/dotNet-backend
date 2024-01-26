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
        private readonly IClubService _clubService;
        private readonly ILogger<ClubController> _logger;

        public ClubController(IClubService clubService, ILogger<ClubController> logger)
        {
            _clubService = clubService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClubResponseDto>>> GetAllClubs()
        {
            _logger.LogInformation("Getting all clubs");
            return await _clubService.GetAllClubsAsync();
        }

        [HttpPost]
        [Authorize(Roles = "Coach")]
        [ValidateModel]
        public async Task<ActionResult<ClubResponseDto>> CreateClubFromCoach([FromBody] ClubRequestDto clubRequestDto)
        {
            string coachUsername = User.Identity.Name;
            _logger.LogInformation("Creating club {} from coach with username: {} ", clubRequestDto, coachUsername);
            return await _clubService.CreateClubAsync(clubRequestDto, coachUsername);
        }
    }
}