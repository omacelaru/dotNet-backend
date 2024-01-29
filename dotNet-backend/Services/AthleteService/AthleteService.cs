using AutoMapper;
using dotNet_backend.Models.Athlete;
using dotNet_backend.Models.Athlete.DTO;
using dotNet_backend.Models.Request.DTO;
using dotNet_backend.Repositories.AthleteRepository;
using dotNet_backend.Repositories.RequestRepository;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;

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

    public async Task<ActionResult<IEnumerable<AthleteCoachNameResponseDto>>> GetAllAthletesAsync()
    {
        _logger.LogInformation("Getting all athletes");
        var athletes = await _athleteRepository.FindAllAthletesAsync();
        return _mapper.Map<List<AthleteCoachNameResponseDto>>(athletes);
    }
    
    public async Task<ActionResult<AthleteCoachNameResponseDto>> GetAthleteByUsernameAsync(string username)
    {
        _logger.LogInformation("Getting athlete by username {username}", username);
        var athlete = await _athleteRepository.FindAthleteByUsernameAsync(username);
        return _mapper.Map<AthleteCoachNameResponseDto>(athlete);
    }
    
    public async Task<ActionResult<IEnumerable<RequestInfoResponseDto>>> GetAthleteRequestsAsync(string username)
    {
        _logger.LogInformation("Getting athlete [me {username}] requests", username);
        var athlete = await _athleteRepository.FindAthleteByUsernameAsync(username);
        var requests = await _requestRepository.FindAllRequestsRequestedByUsernameAsync(athlete.Username);
        return _mapper.Map<List<RequestInfoResponseDto>>(requests);
    }
    
    public async Task<ActionResult<RequestInfoResponseDto>> DeleteAthleteRequestAsync(Guid id, string athleteUsername)
    {
        _logger.LogInformation("Deleting athlete [me {username}] request with id {id}", athleteUsername, id);
        var request = await _requestRepository.FindRequestToAddAthleteToCoachByUsernameAndRequestIdAsync(athleteUsername, id);
        if (request == null)
        {
            _logger.LogError("Request not found for athlete {} and request id {}", athleteUsername, id);
            throw new NotFoundException("Request not found");
        }
        _requestRepository.Delete(request);
        await _requestRepository.SaveAsync();
        return _mapper.Map<RequestInfoResponseDto>(request);
    }
}