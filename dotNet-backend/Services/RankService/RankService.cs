using AutoMapper;
using dotNet_backend.Helpers.PaginationFilter;
using dotNet_backend.Models.Athlete.DTO;
using dotNet_backend.Models.Club.DTO;
using dotNet_backend.Repositories.AthleteRepository;
using dotNet_backend.Repositories.ClubRepository;
using dotNet_backend.Repositories.CoachRepository;
using dotNet_backend.Repositories.CompetitionRepository;
using dotNet_backend.Repositories.ParticipationRepository;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;

namespace dotNet_backend.Services.RankService;

public class RankService : IRankService
{
    private readonly IAthleteRepository _athleteRepository;
    private readonly IClubRepository _clubRepository;
    private readonly ICoachRepository _coachRepository;
    private readonly ICompetitionRepository _competitionRepository;
    private readonly IParticipationRepository _participationRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<RankService> _logger;
    
    public RankService(IAthleteRepository athleteRepository, IClubRepository clubRepository, ICoachRepository coachRepository, ICompetitionRepository competitionRepository, IParticipationRepository participationRepository, IMapper mapper, ILogger<RankService> logger)
    {
        _athleteRepository = athleteRepository;
        _clubRepository = clubRepository;
        _coachRepository = coachRepository;
        _competitionRepository = competitionRepository;
        _participationRepository = participationRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ActionResult<IEnumerable<AthleteCoachNameResponseDto>>> GetAllAthletesAsync(
        PaginationFilter paginationFilter, string sortBy)
    {
        _logger.LogInformation("Getting all athletes for rank with pagination and {}", sortBy);
        var athletes = await _athleteRepository.FindAllAthletesAsync();
        athletes = athletes.OrderBy(athlete => athlete.Points);
        var pagedResults = athletes.Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
            .Take(paginationFilter.PageSize).ToList();
        if(pagedResults == null)
        {
            _logger.LogError("Pagination error");
            throw new NotFoundException("Not paginated");
        }
        return _mapper.Map<List<AthleteCoachNameResponseDto>>(pagedResults);
    }

    public async Task<ActionResult<IEnumerable<ClubResponseWithPointsDto>>> GetAllClubsAndCoachesAsync(
               PaginationFilter paginationFilter, string sortBy)
    {
        //for each club get the points attribute
        //sort them by points
        //return them with pagination
        _logger.LogInformation("Getting clubs by coach points by athletes with pagination and {}", sortBy);
        var clubs = await _clubRepository.FindAllClubsAsync();
        var clubPoints = new List<ClubResponseWithPointsDto>();
        foreach(var club in clubs)
        {
            var coach = await _coachRepository.FindCoachByIdAsync(club.CoachId);
            if(coach == null)
            {
                _logger.LogError("Coach with id {} not found from club {}", club.CoachId, club.Id);
                throw new NotFoundException("Coach not found");
            }
            var athletes = await _athleteRepository.FindAllAthletesAssignedToCoachUsernameAsync(coach.Username);
            var points = athletes.Sum(athlete => athlete.Points);
            clubPoints.Add(new ClubResponseWithPointsDto
            {
                Id = club.Id,
                Name = club.Name,
                CoachName = coach.Name,
                Points = points
            });
            //create a new query to add the points to the club
        }
        clubPoints = clubPoints.OrderBy(club => club.Points).ToList();
        var pagedResults = clubPoints.Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
            .Take(paginationFilter.PageSize).ToList();
        if(pagedResults == null)
        {
            _logger.LogError("Pagination error");
            throw new NotFoundException("Not paginated");
        }
        return _mapper.Map<List<ClubResponseWithPointsDto>>(pagedResults);
    }
    
    
}