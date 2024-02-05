using dotNet_backend.CustomActionFilters;

namespace dotNet_backend.Models.Participation.DTO;

public class ParticipationRequestDto
{
    public List<string> AthletesUsernames { get; set; }
}

[ValidatePozitiveNumber]
public class ParticipationAwardsRequestDto
{
    public int FirstPlace { get; set; } = 0;
    public int SecondPlace { get; set; } = 0;
    public int ThirdPlace { get; set; } = 0;
}