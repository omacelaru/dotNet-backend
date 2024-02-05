using dotNet_backend.Models.User;
using dotNet_backend.Repositories.GenericRepository;

namespace dotNet_backend.Repositories.UserRepository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> FindByEmailAsync(string email);
        Task<User> FindByUsernameAsync(string username);
    }
}
