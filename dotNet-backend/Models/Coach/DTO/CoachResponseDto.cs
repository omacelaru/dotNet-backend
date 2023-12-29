using dotNet_backend.Models.Club.DTO;

namespace dotNet_backend.Models.Coach.DTO
{
    public class CoachResponseDto
    {
        public string Name { get; set; }
        public string ClubName { get; set; }

        public ICollection<string> AthletesNames { get; set; }
    }
}
