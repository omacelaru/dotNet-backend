using dotNet_backend.Data.Exceptions;
using dotNet_backend.Models.User;
using dotNet_backend.Models.User.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using dotNet_backend.Repositories.UserRepository;

namespace dotNet_backend.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _passwordHasher = new PasswordHasher<User>();
            _configuration = configuration;
        }

        public async Task<User> RegisterUserAsync(RegisterDto registerDto)
        {
            var user = new User
            {
                Username = registerDto.Username,
                Email = registerDto.Email
            };

            user.Password = _passwordHasher.HashPassword(user, registerDto.Password);

            _userRepository.Create(user);
            await _userRepository.SaveAsync();
            return user;
        }

        public string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim("EmailConfirmed", user.EmailConfirmed.ToString()),
                }),
                Audience = _configuration["JWT:ValidAudience"],
                Issuer = _configuration["JWT:ValidIssuer"],
                Expires = DateTime.UtcNow.AddMinutes(5), // Token expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<string> GenerateRefreshToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, user.Username),

            }),
                Expires = DateTime.UtcNow.AddDays(1), // Token expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.RefreshToken = tokenHandler.WriteToken(token);
            _userRepository.Update(user);

            await _userRepository.SaveAsync();
            return user.RefreshToken;
        }
        public async Task<object> LoginUserAsync(LoginDto loginDto)
        {
            var user = await _userRepository.FindSingleOrDefaultAsync(u => u.Username == loginDto.Username);

            if (VerifyPassword(user.Password, loginDto.Password))
            {
                var refreshToken = await GenerateRefreshToken(user);
                var accessToken = GenerateJwtToken(user);
                return new { AccessToken = accessToken, RefreshToken = refreshToken };
            }
            else
            {
                throw new AuthorizationException("Invalid credentials.");
            }


        }

        public async Task<object> RefreshTokenAsync(string refreshToken)
        {
            ValidateToken(refreshToken);

            var user = await _userRepository.FindSingleOrDefaultAsync(u => u.RefreshToken == refreshToken);

            if (user != null)
            {
                return new { AccessToken = GenerateJwtToken(user) };
            }
            else
            {
                throw new BadRequestException("Invalid refresh token.");
            }
        }

        public ClaimsPrincipal ValidateToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JWT:Key")),

                ClockSkew = TimeSpan.Zero
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);


                if (!(validatedToken is JwtSecurityToken jwtSecurityToken) ||
                    !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new SecurityTokenException("Invalid token");
                }

                return principal;
            }
            catch (SecurityTokenException)
            {
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