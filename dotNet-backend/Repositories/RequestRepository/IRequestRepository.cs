using dotNet_backend.Models.Request;
using dotNet_backend.Repositories.GenericRepository;

namespace dotNet_backend.Repositories.RequestRepository;

public interface IRequestRepository : IGenericRepository<RequestInfo>
{
    Task<IEnumerable<RequestInfo>> GetRequestsByUsernameAsync(string username);
    Task<RequestInfo> GetRequestByUsernameAsync(string myUsername, string usernameAthlete);
}