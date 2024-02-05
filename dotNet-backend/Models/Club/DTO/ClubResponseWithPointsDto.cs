namespace dotNet_backend.Models.Club.DTO
{
    public class ClubResponseWithPointsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CoachName { get; set; }
        public int Points { get; set; }
    }
}
