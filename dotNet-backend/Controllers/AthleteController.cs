using dotNet_backend.Data.Exceptions;
using dotNet_backend.Models.Athlete.DTO;
using dotNet_backend.Models.Request.DTO;
using dotNet_backend.Services.AthleteService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AthleteController(IAthleteService athleteService) : ControllerBase
    {
        private readonly IAthleteService _athleteService = athleteService;

        /// <summary>
        /// Get all athletes
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AthleteCoachNameResponseDto>>> GetAllAthletes() => 
            await _athleteService.GetAllAthletesAsync();
        
        /// <summary>
        /// Get the current athlete
        /// </summary>
        [HttpGet("me")]
        [Authorize(Roles = "Athlete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<AthleteCoachNameResponseDto>> GetAthlete() => 
            await _athleteService.GetAthleteByUsernameAsync(User.Identity.Name);
        
        /// <summary>
        /// Get the current athlete's requests
        /// </summary>
        [HttpGet("me/requests")]
        [Authorize(Roles = "Athlete")]
        public async Task<ActionResult<IEnumerable<RequestInfoResponseDto>>> GetAthleteRequests() => 
            await _athleteService.GetAthleteRequestsAsync(User.Identity.Name);
        
        /// <summary>
        /// Delete a request of the current athlete
        /// </summary>
        /// <param name="Id"></param>
        /// <response code="200">Request deleted</response>
        /// <response code="404">Request not found</response>
        [HttpDelete("me/requests/{Id:Guid}")]
        [Authorize(Roles = "Athlete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorMessage))]
        public async Task<ActionResult<RequestInfoResponseDto>> DeleteAthleteRequest(Guid Id) => 
            await _athleteService.DeleteAthleteRequestAsync(Id, User.Identity.Name);
    }
}
