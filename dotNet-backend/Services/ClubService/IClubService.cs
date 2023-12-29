using dotNet_backend.Models.Club;
using dotNet_backend.Models.Club.DTO;

namespace dotNet_backend.Services.ClubService
{
    public interface IClubService
    {
        Task<Club> CreateClubAsync(Club club);
        Task<IEnumerable<ClubResponseDto>> GetAllClubsAsync();
    }
}
