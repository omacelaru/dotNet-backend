namespace dotNet_backend.Models.Athlete.DTO
{
    public class AthleteResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CoachName { get; set; }
    }
    
    public class AthleteUsernameResponseDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
    }
}