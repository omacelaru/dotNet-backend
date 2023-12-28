using dotNet_backend.Models.Base;
using System.ComponentModel.DataAnnotations;
using dotNet_backend.Models.User.Enum;

namespace dotNet_backend.Models.User
{
    public class User : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public bool EmailConfirmed { get; set; } = false;
        public string? RefreshToken { get; set; }

        public Role Role { get; set; }

        public Coach.Coach? Coach { get; set; }

        public Athlete.Athlete? Athlete { get; set; }

    }

}
