using dotNet_backend.Models.User;
using dotNet_backend.Repositories.GenericRepository;

namespace dotNet_backend.Repositories.UserRepository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context) { }

        // Add custom methods here
    }
}
