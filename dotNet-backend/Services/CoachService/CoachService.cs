using AutoMapper;
using dotNet_backend.Data.Exceptions;
using dotNet_backend.Models.Club;
using dotNet_backend.Models.Coach;
using dotNet_backend.Models.Coach.DTO;
using dotNet_backend.Models.Request;
using dotNet_backend.Repositories.AthleteRepository;
using dotNet_backend.Repositories.ClubRepository;
using dotNet_backend.Repositories.CoachRepository;
using dotNet_backend.Repositories.RequestRepository;
using dotNet_backend.Repositories.UserRepository;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;

namespace dotNet_backend.Services.CoachService;

public class CoachService : ICoachService
{
    private readonly ICoachRepository _coachRepository;
    private readonly IAthleteRepository _athleteRepository;
    private readonly ILogger<CoachService> _logger;
    private readonly IMapper _mapper;

    public CoachService(ICoachRepository coachRepository, IClubRepository clubRepository,
        IAthleteRepository athleteRepository, ILogger<CoachService> logger, IMapper mapper)
    {
        _coachRepository = coachRepository;
        _athleteRepository = athleteRepository;
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
        var coach = await _coachRepository.FindCoachByIdAsync(id);
        return _mapper.Map<CoachResponseDto>(coach);
    }

    public async Task<ActionResult<CoachResponseDto>> GetCoachByUsernameAsync(string username)
    {
        var coach = await _coachRepository.FindCoachByUsernameAsync(username);
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
}