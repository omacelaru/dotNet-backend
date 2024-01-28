using AutoMapper;
using dotNet_backend.Models.Athlete.DTO;
using dotNet_backend.Repositories.AthleteRepository;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_backend.Services.AthleteService;

public class AthleteService : IAthleteService
{
    private readonly IAthleteRepository _athleteRepository;
    private readonly ILogger<AthleteService> _logger;
    private readonly IMapper _mapper;


    public AthleteService(IAthleteRepository athleteRepository, ILogger<AthleteService> logger, IMapper mapper)
    {
        _athleteRepository = athleteRepository;
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
}