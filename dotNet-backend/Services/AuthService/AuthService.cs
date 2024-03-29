﻿using dotNet_backend.Helpers.GenerateJwt;
using dotNet_backend.Models.User;
using dotNet_backend.Models.User.DTO;
using dotNet_backend.Repositories.UserRepository;
using dotNet_backend.Services.SMTP;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> LoginUserAsync(LoginDto loginDto)
        {
            _logger.LogInformation("Logging in user {}", loginDto);
            var user = await _userRepository.FindSingleOrDefaultAsync(u => u.Username == loginDto.Username);

            if (user == null)
            {
                _logger.LogError("Unauthorized login attempt {}", loginDto);
                throw new UnauthorizedException("Invalid credentials.");
            }
            if (user.EmailConfirmed == false)
            {
                _logger.LogError("Forbidden login attempt {}", loginDto);
                throw new ForbiddenException("Email not confirmed.");
            }
            if (VerifyPassword(user.Password, loginDto.Password))
            {
                var refreshToken = TokenJwt.GenerateRefreshToken(user);
                user.RefreshToken = refreshToken;
                await _userRepository.SaveAsync();
                var accessToken = TokenJwt.GenerateJwtToken(user);
                return new OkObjectResult(new { AccessToken = accessToken, RefreshToken = refreshToken });
            }
            _logger.LogError("Unauthorized login attempt {}", loginDto);
            throw new UnauthorizedException("Invalid credentials.");
        }
        public async Task<IActionResult> RefreshTokenAsync(string refreshToken)
        {
            _logger.LogInformation("Refreshing token {}", refreshToken);
            //TokenJwt.ValidateToken(refreshToken);

            var user = await _userRepository.FindSingleOrDefaultAsync(u => u.RefreshToken == refreshToken);

            if (user != null)
            {
                return new OkObjectResult(new { AccessToken = TokenJwt.GenerateJwtToken(user) });
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

        public async Task<IActionResult> VerifyEmailAsync(string token)
        {
            _logger.LogInformation("Verifying email with token {}", token);
            string token_name = TokenJwt.GetUsernameFromToken(token);
            User _user = await _userRepository.FindByUsernameAsync(token_name);
            _user.EmailConfirmed = true;
            _userRepository.Update(_user);
            await _userRepository.SaveAsync();
            return new OkResult();
        }
    }
}