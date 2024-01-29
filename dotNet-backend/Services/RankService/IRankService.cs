using dotNet_backend.Helpers.PaginationFilter;
using dotNet_backend.Models.Athlete.DTO;
using dotNet_backend.Models.Club.DTO;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_backend.Services.RankService;

public interface IRankService
{
    Task<ActionResult<IEnumerable<AthleteCoachNameResponseDto>>> GetAllAthletesAsync(PaginationFilter paginationFilter, string sortBy);
    Task<ActionResult<IEnumerable<ClubResponseWithPointsDto>>> GetAllClubsAndCoachesAsync(PaginationFilter paginationFilter, string sortBy);
}