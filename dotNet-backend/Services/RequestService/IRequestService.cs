using dotNet_backend.Models.Athlete;
using dotNet_backend.Models.Coach;
using dotNet_backend.Models.Request;
using dotNet_backend.Models.Request.Enum;

namespace dotNet_backend.Services.RequestService;

public interface IRequestService
{
    Task<IEnumerable<RequestInfo>> GetRequestsByUsernameAsync(string username);
    Task<RequestInfo> UpdateRequestStatusAsync(string coachUsername, string usernameAthlete, string requestStatus);
    Task<RequestInfo> CreateRequestAsync(Athlete athlete, Coach coach, RequestType addAthleteToCoach);
}