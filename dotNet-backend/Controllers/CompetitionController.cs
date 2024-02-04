using dotNet_backend.CustomActionFilters;
using dotNet_backend.Models.Athlete.DTO;
using dotNet_backend.Models.Competition.DTO;
using dotNet_backend.Services.CompetitionService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CompetitionController(ICompetitionService competitionService) : ControllerBase
    {
        private readonly ICompetitionService _competitionService = competitionService;

        /// <summary>
        /// Get all competitions
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompetitionResponseDto>>> GetAllCompetitions() =>
            await _competitionService.GetAllCompetitions();
        
        /// <summary>
        /// Create a competition by an admin
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateModel]
        public async Task<ActionResult<CompetitionResponseDto>> CreateCompetition([FromBody] CompetitionRequestDto competitionRequestDto) => 
            await _competitionService.CreateCompetitionAsync(competitionRequestDto);
        
        /// <summary>
        /// Delete a competition by an admin
        /// </summary>
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CompetitionResponseDto>> DeleteCompetition(Guid id) =>
            await _competitionService.DeleteCompetitionAsync(id);
        
        /// <summary>
        /// Get a competition by id
        /// </summary>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CompetitionResponseDto>> GetCompetitionById(Guid id) =>
            await _competitionService.GetCompetitionByIdAsync(id);
        
        /// <summary>
        /// Get all athletes who are participating in a competition by competition id
        /// </summary>
        [HttpGet("{id:guid}/athletes")]
        public async Task<ActionResult<IEnumerable<AthleteCoachNameResponseDto>>> GetCompetitionAthletes(Guid id) =>
            await _competitionService.GetCompetitionAthletesAsync(id);
        
    }
}
