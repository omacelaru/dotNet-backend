using AutoMapper;
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
        
        public async Task<Athlete> RegisterAthleteAsync(AthleteRegisterDto athleteRegisterDto)
        {
            var athlete = _mapper.Map<Athlete>(athleteRegisterDto);
            athlete.Password = _passwordHasher.HashPassword(athlete, athleteRegisterDto.Password);
            athlete.Role = Role.Athlete;

            var token = TokenJwt.GenerateJwtToken(athlete);
            
            var link =  _configuration["AppUrl"] + "/api/auth/confirm?token=" + token;
            
            await _smtpService.SendEmailAsync(athlete.Email, _confirmEmailSubject, string.Format(_confirmEmailBody, link));
            
            _userRepository.Create(athlete);
            await _userRepository.SaveAsync();
            return athlete;
        }

        public async Task<Coach> RegisterCoachAsync(CoachRegisterDto coachRegisterDto)
        {
            if( await _userRepository.GetByEmailAsync(coachRegisterDto.Email) != null)
                throw new ArgumentException("Email already exists!");
            
            var coach = _mapper.Map<Coach>(coachRegisterDto);
            coach.Password = _passwordHasher.HashPassword(coach, coachRegisterDto.Password);
            coach.Role = Role.Coach;
            
            var token = TokenJwt.GenerateJwtToken(coach);
            
            var link =  _configuration["AppUrl"] + "/api/auth/confirm?token=" + token;
            
            _logger.LogInformation("Sending email to {email} with subject {subject}", coach.Email, _confirmEmailSubject);
            await _smtpService.SendEmailAsync(coach.Email, _confirmEmailSubject, string.Format(_confirmEmailBody, link));
            
            _userRepository.Create(coach);
            await _userRepository.SaveAsync();
            return coach;
        }
        
    }
}
