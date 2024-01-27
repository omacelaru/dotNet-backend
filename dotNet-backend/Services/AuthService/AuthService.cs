using dotNet_backend.Data.Exceptions;
using dotNet_backend.Models.User;
using dotNet_backend.Models.User.DTO;
using Microsoft.AspNetCore.Identity;
using dotNet_backend.Helpers.GenerateJwt;
using dotNet_backend.Repositories.UserRepository;
using dotNet_backend.Services.SMTP;
using SendGrid.Helpers.Errors.Model;

namespace dotNet_backend.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;
        private readonly ISMTPService _smtpService;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IUserRepository userRepository, IConfiguration configuration, ISMTPService smtpService, ILogger<AuthService> logger)
        {
            _userRepository = userRepository;
            _passwordHasher = new PasswordHasher<User>();
            _configuration = configuration;
            //generate new SMTPService
            _smtpService = smtpService;
            _logger = logger;
        }
        
        public async Task<object> LoginUserAsync(LoginDto loginDto)
        {
            var user = await _userRepository.FindSingleOrDefaultAsync(u => u.Username == loginDto.Username);

            if (VerifyPassword(user.Password, loginDto.Password))
            {
                var refreshToken = TokenJwt.GenerateRefreshToken(user);
                user.RefreshToken = refreshToken;
                await _userRepository.SaveAsync();
                var accessToken = TokenJwt.GenerateJwtToken(user);
                return new { AccessToken = accessToken, RefreshToken = refreshToken };
            }
            else
            {
                _logger.LogError("Unauthorized login attempt {}", loginDto);
                throw new UnauthorizedException("Invalid credentials.");
            }
        }
        public async Task<object> RefreshTokenAsync(string refreshToken)
        {
            TokenJwt.ValidateToken(refreshToken);

            var user = await _userRepository.FindSingleOrDefaultAsync(u => u.RefreshToken == refreshToken);

            if (user != null)
            {
                return new { AccessToken = TokenJwt.GenerateJwtToken(user) };
            }
            else
            {
                _logger.LogError("Bad request refreshing token {}", refreshToken);
                throw new BadRequestException("Invalid refresh token.");
            }
        }
        
        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(new User(), hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success ||
                   result == PasswordVerificationResult.SuccessRehashNeeded;
        }
    }
}