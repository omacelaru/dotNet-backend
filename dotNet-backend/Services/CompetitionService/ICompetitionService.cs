using dotNet_backend.Models.Competition.DTO;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_backend.Services.CompetitionService;

public interface ICompetitionService
{
    Task<ActionResult<IEnumerable<CompetitionResponseDto>>> GetAllCompetitions();
    Task<ActionResult<CompetitionResponseDto>> CreateCompetitionAsync(CompetitionRequestDto competitionRequestDto);
}