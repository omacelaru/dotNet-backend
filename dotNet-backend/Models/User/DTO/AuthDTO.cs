namespace dotNet_backend.Models.User.DTO
{
    public class RegisterDto
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public string Email { get; set; }
        // Other registration properties
    }

    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class TokenDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}

