using AutoMapper;
using dotNet_backend.Models.Athlete;
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
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;

namespace dotNet_backend.Services.ParticipationService;

public class ParticipationService :IParticipationService
{
    private readonly ICompetitionRepository _competitionRepository;
    private readonly IParticipationRepository _participationRepository;
    private readonly IAthleteRepository _athleteRepository;
    private readonly ILogger<ParticipationService> _logger;
    private readonly IMapper _mapper;


    public ParticipationService(ICompetitionRepository competitionRepository, IParticipationRepository participationRepository,IAthleteRepository athleteRepository, ILogger<ParticipationService> logger, IMapper mapper)
    {
        _competitionRepository = competitionRepository;
        _participationRepository = participationRepository;
        _athleteRepository = athleteRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<ActionResult<ParticipationResponseDto>> AddAthletesToCompetitionAsync(Guid competitionId,ParticipationRequestDto participationRequestDto, string coachUsername)
    {
        var competition = await _competitionRepository.FindCompetitionByIdAsync(competitionId);
        if (competition == null)
        {
            _logger.LogError("Competition with id {competitionId} not found", competitionId);
            throw new NotFoundException("Competition not found");
        }
        var athletesUsernames = participationRequestDto.AthletesUsernames;
        var athletes = new List<Athlete>();
        foreach (var athleteUsername in athletesUsernames)
        {
            var athlete = await _athleteRepository.FindAthleteByUsernameAsync(athleteUsername);
            if (athlete == null)
            {
                _logger.LogError("Athlete with username {athleteUsername} not found", athleteUsername);
                throw new NotFoundException($"Athlete with username {athleteUsername} not found");
            }
            if(athlete.Coach == null || athlete.Coach.Username != coachUsername)
            {
                _logger.LogError("Athlete with username {athleteUsername} is not coached by {coachUsername}", athleteUsername, coachUsername);
                throw new BadRequestException($"You don't train the athlete with the username {athleteUsername}");
            }
            athletes.Add(athlete);
        }
        foreach (var athlete in athletes)
        {
            var participation = new Participation
            {
                AthleteId = athlete.Id,
                CompetitionId = competition.Id
            };
            await _participationRepository.CreateAsync(participation);
        } 
        await _participationRepository.SaveAsync();
        var response = new ParticipationResponseDto
        {
            Competition = new KeyValuePair<Guid, string>(competition.Id, competition.Name),
            Athletes = athletes.Select(a => new KeyValuePair<Guid, string>(a.Id, a.Username)).ToList()
        };
        return response;
    }
}