using dotNet_backend.CustomActionFilters;
using dotNet_backend.Models.Club.DTO;
using dotNet_backend.Services.ClubService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClubController(IClubService clubService) : ControllerBase
    {
        private readonly IClubService _clubService = clubService;
        
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