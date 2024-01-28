using dotNet_backend.CustomActionFilters;

namespace dotNet_backend.Models.Competition.DTO;

[ValidateCompetition]
public class CompetitionRequestDto
{
    public string Name { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}