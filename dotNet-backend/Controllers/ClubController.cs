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
        
        /// <summary>
        /// Get all clubs
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Clubs found</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClubResponseDto>>> GetAllClubs() =>
            await _clubService.GetAllClubsAsync();

        /// <summary>
        /// Create a club from a coach account
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Club created</response>
        [HttpPost]
        [Authorize(Roles = "Coach")]
        [ValidateModel]
        public async Task<ActionResult<ClubResponseDto>> CreateClubFromCoach([FromBody] ClubRequestDto clubRequestDto) => 
            await _clubService.CreateClubAsync(clubRequestDto, User.Identity.Name);
        
        /// <summary>
        /// Delete a club by a coach
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Club deleted</response>
        [HttpDelete]
        [Authorize(Roles = "Coach")]
        public async Task<ActionResult<ClubResponseDto>> DeleteClubFromCoach() => 
            await _clubService.DeleteClubAsync(User.Identity.Name);
        
        /// <summary>
        /// Update a club name by a coach
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Club updated</response>
        [HttpPatch]
        [Authorize(Roles = "Coach")]
        [ValidateModel]
        public async Task<ActionResult<ClubResponseDto>> UpdateClubFromCoach([FromBody] ClubRequestDto clubRequestDto) =>
            await _clubService.UpdateClubAsync(clubRequestDto, User.Identity.Name);
    }
}