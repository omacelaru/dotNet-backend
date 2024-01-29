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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompetitionResponseDto>>> GetAllCompetitions() =>
            await _competitionService.GetAllCompetitions();
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateModel]
        public async Task<ActionResult<CompetitionResponseDto>> CreateCompetition([FromBody] CompetitionRequestDto competitionRequestDto) => 
            await _competitionService.CreateCompetitionAsync(competitionRequestDto);
        
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CompetitionResponseDto>> DeleteCompetition(Guid id) =>
            await _competitionService.DeleteCompetitionAsync(id);
        
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CompetitionResponseDto>> GetCompetitionById(Guid id) =>
            await _competitionService.GetCompetitionByIdAsync(id);
        
        [HttpGet("{id:guid}/athletes")]
        public async Task<ActionResult<IEnumerable<AthleteCoachNameResponseDto>>> GetCompetitionAthletes(Guid id) =>
            await _competitionService.GetCompetitionAthletesAsync(id);
        
    }
}
