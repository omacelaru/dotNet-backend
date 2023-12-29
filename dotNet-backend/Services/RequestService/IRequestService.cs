using dotNet_backend.Models.Athlete;
using dotNet_backend.Models.Coach;
using dotNet_backend.Models.Request;
using dotNet_backend.Models.Request.Enum;

namespace dotNet_backend.Services.RequestService;

public interface IRequestService
{
    Task JoinCoachAsync(Coach coach, Athlete athlete);
    Task<IEnumerable<RequestInfo>> GetRequestsByUsernameAsync(string username);
    Task<RequestInfo> UpdateRequestStatusAsync(string myUsername, string usernameAthlete, RequestStatus requestStatus);
}