namespace dotNet_backend.Models.Competition.DTO
{
    public class CompetitionResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfParticipants { get; set; }
        public int DayLeft { get; set; }
    }

    public class CompetitionNameResponseDto
    {       
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}