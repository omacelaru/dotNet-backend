using dotNet_backend.Models.Base;

namespace dotNet_backend.Models.Club
{
    public class Club : BaseEntity
    {
        public string Name { get; set; }
        public Guid CoachId { get; set; }
        public Coach.Coach Coach { get; set; }
    }
}