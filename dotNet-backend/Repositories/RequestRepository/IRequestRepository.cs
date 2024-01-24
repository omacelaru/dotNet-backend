using dotNet_backend.Models.Request;
using dotNet_backend.Repositories.GenericRepository;

namespace dotNet_backend.Repositories.RequestRepository;

public interface IRequestRepository : IGenericRepository<RequestInfo>
{
    Task<IEnumerable<RequestInfo>> FindRequestsByUsernameAsync(string username);
    Task<RequestInfo> FindRequestByUsernamesAsync(string myUsername, string usernameAthlete);
}