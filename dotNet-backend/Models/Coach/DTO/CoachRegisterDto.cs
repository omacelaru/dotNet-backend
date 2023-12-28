using dotNet_backend.Models.User.DTO;

namespace dotNet_backend.Models.Coach.DTO
{
    public class CoachRegisterDto : RegisterDto
    {
        public string Name { get; set; }
    }
}
