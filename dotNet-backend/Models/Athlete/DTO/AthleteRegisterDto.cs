using System.ComponentModel.DataAnnotations;
using dotNet_backend.Models.User.DTO;

namespace dotNet_backend.Models.Athlete.DTO
{
    public class AthleteRegisterDto : RegisterDto
    {
        [Required]
        public string Name { get; set; }
    }
}
