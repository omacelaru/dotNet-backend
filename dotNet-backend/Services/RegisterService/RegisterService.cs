using AutoMapper;
using dotNet_backend.Data.Exceptions;
using dotNet_backend.Helpers.GenerateJwt;
using dotNet_backend.Models.User.DTO;
using dotNet_backend.Models.User;
using dotNet_backend.Repositories.UserRepository;
using dotNet_backend.Services.SMTP;
using Microsoft.AspNetCore.Identity;
using dotNet_backend.Models.Athlete.DTO;
using dotNet_backend.Models.Athlete;
using dotNet_backend.Models.Coach;
using dotNet_backend.Models.Coach.DTO;
using dotNet_backend.Models.User.Enum;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;

namespace dotNet_backend.Services.RegisterService
{
    public class RegisterService : IRegisterService
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;
        private readonly ISMTPService _smtpService;
        private readonly ILogger<RegisterService> _logger;
        private readonly IMapper _mapper;
        
        private string _confirmEmailSubject = "Confirm your account";
        private string _confirmEmailBody = "<a href=\"{0}\">Confirm your account</a>";

        public RegisterService(IUserRepository userRepository, IConfiguration configuration, ISMTPService smtpService, ILogger<RegisterService> logger, IMapper mapper)
        {
            _userRepository = userRepository;
            _passwordHasher = new PasswordHasher<User>();
            _configuration = configuration;
            _smtpService = smtpService;
            _logger = logger;
            _mapper = mapper;
        }
        
        public async Task<ActionResult<AthleteResponseDto>> RegisterAthleteAsync(RegisterDto athleteRegisterDto)
        {
            _logger.LogInformation("Registering athlete {}", athleteRegisterDto);
            var athlete = _mapper.Map<Athlete>(athleteRegisterDto);
            athlete.Role = Role.Athlete;
            await RegisterUserAsync(athlete);
            return _mapper.Map<AthleteResponseDto>(athlete);
        }

        public async Task<ActionResult<CoachResponseDto>> RegisterCoachAsync(RegisterDto coachRegisterDto)
        {
            _logger.LogInformation("Registering coach {}", coachRegisterDto);
            var coach = _mapper.Map<Coach>(coachRegisterDto);
            coach.Role = Role.Coach;
            await RegisterUserAsync(coach);
            return _mapper.Map<CoachResponseDto>(coach);
        }
        
        public async Task<IActionResult> RegisterAdminAsync(RegisterDto adminRegisterDto)
        {
            _logger.LogInformation("Registering admin {}", adminRegisterDto);
            var admin = _mapper.Map<User>(adminRegisterDto);
            admin.Role = Role.Admin;
            await RegisterUserAsync(admin);
            return new OkResult();
        }
        
        private async Task RegisterUserAsync(User user)
        {
            if (await _userRepository.FindByEmailAsync(user.Email) != null)
            {
                _logger.LogError("Email already exists registering user {}", user);
                throw new BadRequestException("Email is already used.");
            }
            if (await _userRepository.FindByUsernameAsync(user.Username) != null)
            {
                _logger.LogError("Username already exists registering user {}", user);
                throw new BadRequestException("Username is already used.");
            }
            
            user.Password = _passwordHasher.HashPassword(user, user.Password);
            
            var token = TokenJwt.GenerateJwtToken(user);
            
            var link =  _configuration["AppUrl"] + "/api/auth/confirm?token=" + token;
            
            _logger.LogInformation("Sending email to {email} with subject {subject}", user.Email, _confirmEmailSubject);
            //await _smtpService.SendEmailAsync(user.Email, _confirmEmailSubject, string.Format(_confirmEmailBody, link));
            
            _userRepository.Create(user);
            await _userRepository.SaveAsync();
        }
        
    }
}
