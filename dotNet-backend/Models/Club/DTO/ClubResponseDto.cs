namespace dotNet_backend.Models.Club.DTO
{
    public class ClubResponseDto
    {
        public string Name { get; set; }

        public ICollection<string> CoachesNames { get; set; }
        
    }
}
