
using dotNet_backend.Models.Base;

namespace dotNet_backend.Models.Coach
{
    public class Coach : BaseEntity
    {
        public string Name { get; set; }

        public Club.Club Club { get; set; }

        public User.User User { get; set; }
    }
}
