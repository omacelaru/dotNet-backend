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
    private readonly IClubRepository _clubRepository;
    private readonly IAthleteRepository _athleteRepository;
    private readonly IMapper _mapper;

    public CoachService(ICoachRepository coachRepository, IClubRepository clubRepository,
        IAthleteRepository athleteRepository, IMapper mapper)
    {
        _coachRepository = coachRepository;
        _clubRepository = clubRepository;
        _athleteRepository = athleteRepository;
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

    public async Task AddAthleteToCoach(string athleteUsername, string coachUsername)
    {
        var athlete = await _athleteRepository.FindAthleteByUsernameAsync(athleteUsername);
        var coach = await _coachRepository.FindCoachByUsernameAsync(coachUsername);
        if (athlete == null || coach == null)
        {
            throw new NotFoundException("Athlete or coach not found");
        }

        athlete.Coach = coach;
        _athleteRepository.Update(athlete);
        await _athleteRepository.SaveAsync();
    }
}