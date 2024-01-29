using AutoMapper;
using dotNet_backend.Models.Athlete;
using dotNet_backend.Models.Athlete.DTO;
using dotNet_backend.Models.Competition;
using dotNet_backend.Models.Competition.DTO;
using dotNet_backend.Models.Participation;
using dotNet_backend.Models.Participation.DTO;
using dotNet_backend.Models.User;
using dotNet_backend.Models.User.Enum;
using dotNet_backend.Repositories.AthleteRepository;
using dotNet_backend.Repositories.CompetitionRepository;
using dotNet_backend.Repositories.ParticipationRepository;
using dotNet_backend.Repositories.UserRepository;
using dotNet_backend.Services.AthleteService;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;

namespace dotNet_backend.Services.ParticipationService;

public class ParticipationService : IParticipationService
{
    private readonly ICompetitionRepository _competitionRepository;
    private readonly IParticipationRepository _participationRepository;
    private readonly IAthleteRepository _athleteRepository;
    private readonly IAthleteService _athleteService;
    private readonly ILogger<ParticipationService> _logger;
    private readonly IMapper _mapper;


    public ParticipationService(ICompetitionRepository competitionRepository,
        IParticipationRepository participationRepository, IAthleteRepository athleteRepository,
        IAthleteService athleteService, ILogger<ParticipationService> logger, IMapper mapper)
    {
        _competitionRepository = competitionRepository;
        _participationRepository = participationRepository;
        _athleteRepository = athleteRepository;
        _athleteService = athleteService;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<ActionResult<ParticipationResponseDto>> AddAthleteToCompetitionAsync(Guid competitionId,
        string athleteUsername, string coachUsername)
    {
        _logger.LogInformation("Adding athlete {athleteUsername} to competition with id {competitionId}",
            athleteUsername, competitionId);
        var competition = await _competitionRepository.FindCompetitionByIdAsync(competitionId);
        if (competition == null)
        {
            _logger.LogError("Competition with id {competitionId} not found", competitionId);
            throw new NotFoundException("Competition not found");
        }

        var athlete = await _athleteRepository.FindAthleteByUsernameAsync(athleteUsername);
        if (athlete == null)
        {
            _logger.LogError("Athlete with username {athleteUsername} not found", athleteUsername);
            throw new NotFoundException($"Athlete with username {athleteUsername} not found");
        }

        if (athlete.Coach == null || athlete.Coach.Username != coachUsername)
        {
            _logger.LogError("Athlete with username {athleteUsername} is not coached by {coachUsername}",
                athleteUsername, coachUsername);
            throw new BadRequestException($"You don't train the athlete with the username {athleteUsername}");
        }

        if (athlete.Participations.Any(p => p.CompetitionId == competitionId))
        {
            _logger.LogError(
                "Athlete with username {athleteUsername} is already participating in the competition with id {competitionId}",
                athleteUsername, competitionId);
            throw new BadRequestException(
                $"Athlete with username {athleteUsername} is already participating in this competition.");
        }

        var participation = new Participation
        {
            AthleteId = athlete.Id,
            CompetitionId = competition.Id
        };
        athlete.Participations.Add(participation);
        competition.Participations.Add(participation);
        await _participationRepository.CreateAsync(participation);
        await _participationRepository.SaveAsync();

        var response = new ParticipationResponseDto
        {
            Competition = _mapper.Map<CompetitionNameResponseDto>(competition),
            Athletes = _mapper.Map<AthleteUsernameResponseDto>(athlete)
        };
        return response;
    }

    public async Task<ActionResult<ParticipationAthleteWithAwardsResponseDto>>
        UpdateAthleteParticipationWithAwardsAsync(
            Guid competitionId, string athleteUsername, ParticipationAwardsRequestDto participationAwardsRequestDto,
            string coachUsername)
    {
        _logger.LogInformation(
            "Updating athlete with username {athleteUsername} participation in competition with id {competitionId}",
            athleteUsername, competitionId);
        var competition = await _competitionRepository.FindCompetitionByIdAsync(competitionId);
        if (competition == null)
        {
            _logger.LogError("Competition with id {competitionId} not found", competitionId);
            throw new NotFoundException("Competition not found");
        }

        var athlete = await _athleteRepository.FindAthleteByUsernameAsync(athleteUsername);
        if (athlete == null)
        {
            _logger.LogError("Athlete with username {athleteUsername} not found", athleteUsername);
            throw new NotFoundException($"Athlete with username {athleteUsername} not found");
        }

        if (athlete.Coach == null || athlete.Coach.Username != coachUsername)
        {
            _logger.LogError("Athlete with username {athleteUsername} is not coached by {coachUsername}",
                athleteUsername,
                coachUsername);
            throw new BadRequestException($"You don't train the athlete with the username {athleteUsername}");
        }

        if (!athlete.Participations.Any(p => p.CompetitionId == competitionId))
        {
            _logger.LogError(
                "Athlete with username {athleteUsername} is not participating in the competition with id {competitionId}",
                athleteUsername, competitionId);
            throw new BadRequestException(
                $"Athlete with username {athleteUsername} is not participating in this competition.");
        }

        var participation =
            await _participationRepository.FindParticipationByAthleteIdAndCompetitionId(athlete.Id, competitionId);
        participation.FirstPlace = participationAwardsRequestDto.FirstPlace;
        participation.SecondPlace = participationAwardsRequestDto.SecondPlace;
        participation.ThirdPlace = participationAwardsRequestDto.ThirdPlace;
        _participationRepository.Update(participation);
        await _participationRepository.SaveAsync();

        var points = participation.FirstPlace * 5 + participation.SecondPlace * 2 + participation.ThirdPlace;
        athlete.Points += points;
        _athleteRepository.Update(athlete);
        await _athleteRepository.SaveAsync();

        return _mapper.Map<ParticipationAthleteWithAwardsResponseDto>(participation);
    }
}