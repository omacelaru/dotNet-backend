using dotNet_backend.Models.Participation.DTO;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_backend.Services.ParticipationService;

public interface IParticipationService
{
    Task<ActionResult<ParticipationResponseDto>> AddAthleteToCompetitionAsync(Guid competitionId, string athleteUsername, string coachUsername);
    Task<ActionResult<ParticipationAthleteWithAwardsResponseDto>> UpdateAthleteParticipationWithAwardsAsync(Guid competitionId, string athleteUsername, ParticipationAwardsRequestDto participationAwardsRequestDto, string coachUsername);
}