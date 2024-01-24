using AutoMapper;
using dotNet_backend.Controllers;
using dotNet_backend.Data.Exceptions;
using dotNet_backend.Models.Club;
using dotNet_backend.Models.Club.DTO;
using dotNet_backend.Models.Coach;
using dotNet_backend.Repositories.ClubRepository;
using dotNet_backend.Repositories.CoachRepository;

namespace dotNet_backend.Services.ClubService
{
    public class ClubService : IClubService
    {
        private readonly IClubRepository _clubRepository;
        private readonly ICoachRepository _coachRepository;
        private readonly IMapper _mapper;

        public ClubService(IClubRepository clubRepository, ICoachRepository coachRepository, IMapper mapper)
        {
            _clubRepository = clubRepository;
            _coachRepository = coachRepository;
            _mapper = mapper;
        }

        public async Task<Club> CreateClubAsync(Club club)
        {
            await _clubRepository.CreateAsync(club);
            await _clubRepository.SaveAsync();
            return club;
        }

        public async Task<IEnumerable<Club>> GetAllClubsAsync()
        {
            return await _clubRepository.GetAllClubsAsync();
        }

        public async Task UpdateClubAsync(Club newClub)
        {
            _clubRepository.Update(newClub);
            await _clubRepository.SaveAsync();
        }
        
    }
}
