using AutoMapper;
using dotNet_backend.Models.Athlete.DTO;
using dotNet_backend.Models.Request.DTO;
using dotNet_backend.Repositories.AthleteRepository;
using dotNet_backend.Repositories.RequestRepository;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_backend.Services.AthleteService;

public class AthleteService : IAthleteService
{
    private readonly IAthleteRepository _athleteRepository;
    private readonly IRequestRepository _requestRepository;
    private readonly ILogger<AthleteService> _logger;
    private readonly IMapper _mapper;


    public AthleteService(IAthleteRepository athleteRepository,IRequestRepository requestRepository, ILogger<AthleteService> logger, IMapper mapper)
    {
        _athleteRepository = athleteRepository;
        _requestRepository = requestRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<ActionResult<IEnumerable<AthleteResponseDto>>> GetAllAthletesAsync()
    {
        _logger.LogInformation("Getting all athletes");
        var athletes = await _athleteRepository.GetAllAthletesAsync();
        return _mapper.Map<List<AthleteResponseDto>>(athletes);
    }
    
    public async Task<ActionResult<AthleteResponseDto>> GetAthleteByUsernameAsync(string username)
    {
        _logger.LogInformation("Getting athlete by username {username}", username);
        var athlete = await _athleteRepository.FindAthleteByUsernameAsync(username);
        return _mapper.Map<AthleteResponseDto>(athlete);
    }
    
    public async Task<ActionResult<IEnumerable<RequestInfoResponseDto>>> GetAthleteRequestsAsync(string username)
    {
        _logger.LogInformation("Getting athlete [me {username}] requests", username);
        var athlete = await _athleteRepository.FindAthleteByUsernameAsync(username);
        var requests = await _requestRepository.FindAllRequestsRequestedByUsernameAsync(athlete.Username);
        return _mapper.Map<List<RequestInfoResponseDto>>(requests);
    }
    
    
}