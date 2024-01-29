using System.ComponentModel.DataAnnotations;
using dotNet_backend.Models.Competition.DTO;

namespace dotNet_backend.CustomActionFilters;

public class ValidateCompetitionAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var competition = value as CompetitionRequestDto;
        if (string.IsNullOrWhiteSpace(competition.Name) || competition.Name.Length < 5)
        {
            return new ValidationResult("Competition name must be at least 5 characters long");
        }
        if (competition.StartDate.ToDateTime(new TimeOnly(9,0,0)) < DateTime.Now)
        {
            return new ValidationResult("Competition date must be in the future");
        }
        if (competition.EndDate < competition.StartDate)
        {
            return new ValidationResult("Competition end date must be after start date");
        }
        return ValidationResult.Success;
    }
}