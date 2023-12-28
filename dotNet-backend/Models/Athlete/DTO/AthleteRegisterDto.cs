using dotNet_backend.Models.User.DTO;

namespace dotNet_backend.Models.Athlete.DTO
{
    public class AthleteRegisterDto : RegisterDto
    {
        public string Name { get; set; }
    }
}
