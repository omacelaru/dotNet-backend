﻿using dotNet_backend.Models.User;
using Microsoft.IdentityModel.Tokens;
using SendGrid.Helpers.Errors.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace dotNet_backend.Helpers.GenerateJwt
{
    public class TokenJwt
    {
        public static readonly IConfiguration _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        public static String GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim("EmailConfirmed", user.EmailConfirmed.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Audience = _configuration["JWT:ValidAudience"],
                Issuer = _configuration["JWT:ValidIssuer"],
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static String GenerateRefreshToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),

                }),
                Expires = DateTime.UtcNow.AddDays(1), // Token expiration time
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static string GetUsernameFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            return securityToken?.Claims.First(claim => claim.Type == "unique_name").Value;
        }


        public static ClaimsPrincipal ValidateToken(string token)
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
                    !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                        StringComparison.InvariantCultureIgnoreCase))
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
    }
}
