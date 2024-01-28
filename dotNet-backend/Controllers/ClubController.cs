using AutoMapper;
using dotNet_backend.CustomActionFilters;
using dotNet_backend.Data.Exceptions;
using dotNet_backend.Models.Club;
using dotNet_backend.Models.Club.DTO;
using dotNet_backend.Models.Coach;
using dotNet_backend.Services.ClubService;
using dotNet_backend.Services.CoachService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;

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
        public async Task<ActionResult<IEnumerable<ClubResponseDto>>> GetAllClubs() =>
            await _clubService.GetAllClubsAsync();

        [HttpPost]
        [Authorize(Roles = "Coach")]
        [ValidateModel]
        public async Task<ActionResult<ClubResponseDto>> CreateClubFromCoach([FromBody] ClubRequestDto clubRequestDto) => 
            await _clubService.CreateClubAsync(clubRequestDto, User.Identity.Name);
        
        
        [HttpDelete]
        [Authorize(Roles = "Coach")]
        public async Task<ActionResult<ClubResponseDto>> DeleteClubFromCoach() => 
            await _clubService.DeleteClubAsync(User.Identity.Name);
        
        [HttpPatch]
        [Authorize(Roles = "Coach")]
        [ValidateModel]
        public async Task<ActionResult<ClubResponseDto>> UpdateClubFromCoach([FromBody] ClubRequestDto clubRequestDto) =>
            await _clubService.UpdateClubAsync(clubRequestDto, User.Identity.Name);
    }
}