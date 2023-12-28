using dotNet_backend.Models.Base;

namespace dotNet_backend.Models.Athlete
{
    public class Athlete : BaseEntity
    {
        public string Name { get; set; }

        public Coach.Coach Coach { get; set; }

        public User.User User { get; set; }
        public Guid UserId { get; set; }
    }
}
