using AutoMapper;
using dotNet_backend.Controllers;
using dotNet_backend.Models.Club;
using dotNet_backend.Repositories.ClubRepository;

namespace dotNet_backend.Services.ClubService
{
    public class ClubService : IClubService
    {
        private readonly IClubRepository _clubRepository;
        private readonly IMapper _mapper;

        public ClubService(IClubRepository clubRepository, IMapper mapper)
        {
            _clubRepository = clubRepository;
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
            return await _clubRepository.GetAllAsync();
        }


    }
}
