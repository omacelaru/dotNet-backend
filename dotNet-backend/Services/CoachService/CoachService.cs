using AutoMapper;
using dotNet_backend.Models.Athlete.DTO;
using dotNet_backend.Models.Coach.DTO;
using dotNet_backend.Models.Participation.DTO;
using dotNet_backend.Models.Request.DTO;
using dotNet_backend.Models.Request.Enum;
using dotNet_backend.Repositories.AthleteRepository;
using dotNet_backend.Repositories.CoachRepository;
using dotNet_backend.Repositories.ParticipationRepository;
using dotNet_backend.Repositories.RequestRepository;
using dotNet_backend.Services.RequestService;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;

namespace dotNet_backend.Services.CoachService;

public class CoachService : ICoachService
{
    private readonly ICoachRepository _coachRepository;
    private readonly IRequestRepository _requestRepository;
    private readonly IAthleteRepository _athleteRepository;
    private readonly IParticipationRepository _participationRepository;
    private readonly ILogger<CoachService> _logger;
    private readonly IMapper _mapper;

    public CoachService(ICoachRepository coachRepository,IRequestRepository requestRepository,
        IAthleteRepository athleteRepository,IParticipationRepository participationRepository, ILogger<CoachService> logger, IMapper mapper)
    {
        _coachRepository = coachRepository;
        _requestRepository = requestRepository;
        _athleteRepository = athleteRepository;
        _participationRepository = participationRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<ActionResult<IEnumerable<CoachResponseDto>>> GetAllCoachesAsync()
    {
        
        var coaches = await _coachRepository.FindAllCoachesAsync();
        return _mapper.Map<List<CoachResponseDto>>(coaches);
    }

    public async Task<ActionResult<CoachResponseDto>> GetCoachByIdAsync(Guid id)
    {
        _logger.LogInformation("Getting coach with id {}", id);
        var coach = await _coachRepository.FindCoachByIdAsync(id);
        return _mapper.Map<CoachResponseDto>(coach);
    }

    public async Task<ActionResult<CoachResponseDto>> GetCoachByUsernameAsync(string coachUsername)
    {
        _logger.LogInformation("Getting coach with username {}", coachUsername);
        var coach = await _coachRepository.FindCoachByUsernameAsync(coachUsername);
        return _mapper.Map<CoachResponseDto>(coach);
    }

    public async Task AddAthleteToCoachAsync(string athleteUsername, string coachUsername)
    {
        var coach = await _coachRepository.FindCoachByUsernameAsync(coachUsername);
        var athlete = await _athleteRepository.FindAthleteByUsernameAsync(athleteUsername);
        if (athlete == null)
        {
            _logger.LogError("Athlete not found with username {}", athleteUsername);
            throw new NotFoundException("Athlete not found");
        }
        coach.Athletes.Add(athlete);
        _coachRepository.Update(coach);
        await _coachRepository.SaveAsync();
    }
    
    public async Task<ActionResult<IEnumerable<RequestInfoResponseDto>>> GetCoachRequestsByUsernameAsync(string coachUsername)
    {
        _logger.LogInformation("Getting requests for coach {}", coachUsername);
        var coach = await _coachRepository.FindCoachByUsernameAsync(coachUsername);
        var requests = await _requestRepository.FindAllRequestsAssignedToUsernameAsync(coach.Username);
        return _mapper.Map<List<RequestInfoResponseDto>>(requests);
    }
    
    public async Task<ActionResult<IEnumerable<AthleteUsernameResponseDto>>> GetCoachAthletesByUsernameAsync(string coachUsername)
    {
        _logger.LogInformation("Getting athletes for coach {}", coachUsername);
        var coach = await _coachRepository.FindCoachByUsernameAsync(coachUsername);
        var athletes = await _athleteRepository.FindAllAthletesAssignedToCoachUsernameAsync(coach.Username);
        return _mapper.Map<List<AthleteUsernameResponseDto>>(athletes);
    }
    
    public async Task<ActionResult<IEnumerable<ParticipationAthleteWithAwardsResponseDto>>> GetAthletesForCompetitionByIdAndCoachUsernameAsync(Guid competitionId, string coachUsername)
    {
        _logger.LogInformation("Getting athletes for coach {} for competition with id {}", coachUsername, competitionId);
        var competitionAthletes = await _participationRepository.FindAllAthletesForCompetitionByIdAndCoachUsernameAsync(competitionId, coachUsername);
        return _mapper.Map<List<ParticipationAthleteWithAwardsResponseDto>>(competitionAthletes);
    }
    
}