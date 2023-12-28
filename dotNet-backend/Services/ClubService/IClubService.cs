using dotNet_backend.Models.Club;

namespace dotNet_backend.Services.ClubService
{
    public interface IClubService
    {
        Task<Club> CreateClubAsync(Club club);
        Task<IEnumerable<Club>> GetAllClubsAsync();
    }
}
