using dotNet_backend.Models.Club;
using dotNet_backend.Repositories.GenericRepository;

namespace dotNet_backend.Repositories.ClubRepository
{
    public interface IClubRepository : IGenericRepository<Club>
    {
        Task<IEnumerable<Club>> FindAllClubsAsync();
    }
}
