using System.ComponentModel.DataAnnotations;
using dotNet_backend.CustomActionFilters;

namespace dotNet_backend.Models.User.DTO
{
    public class RegisterDto
    {
        [ValidateUsername]
        public string Username { get; set; }
        [ValidatePassword]
        public string Password { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address"), Required] 
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
    }

    public class LoginDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class TokenDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}

