namespace dotNet_backend.Models.Coach.DTO
{
    public class CoachResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ClubName { get; set; }
        public ICollection<string> AthletesNames { get; set; }
    }
}
