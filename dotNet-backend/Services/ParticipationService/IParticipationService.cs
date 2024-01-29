using dotNet_backend.Models.Participation.DTO;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_backend.Services.ParticipationService;

public interface IParticipationService
{
    Task<ActionResult<ParticipationResponseDto>> AddAthletesToCompetitionAsync(Guid competitionId, ParticipationRequestDto participationRequestDto, string coachUsername);
    Task<ActionResult<ParticipationAthleteWithAwardsResponseDto>> UpdateAthleteParticipationWithAwardsAsync(Guid competitionId, string athleteUsername, ParticipationAwardsRequestDto participationAwardsRequestDto, string coachUsername);
}