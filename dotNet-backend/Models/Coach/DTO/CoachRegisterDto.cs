using System.ComponentModel.DataAnnotations;
using dotNet_backend.Models.User.DTO;

namespace dotNet_backend.Models.Coach.DTO
{
    public class CoachRegisterDto : RegisterDto
    {
        [Required]
        public string Name { get; set; }
    }
}
