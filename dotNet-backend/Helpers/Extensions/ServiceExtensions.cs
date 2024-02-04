using System.Reflection;
using System.Text;
using dotNet_backend.Repositories.AthleteRepository;
using dotNet_backend.Repositories.ClubRepository;
using dotNet_backend.Repositories.CoachRepository;
using dotNet_backend.Repositories.CompetitionRepository;
using dotNet_backend.Repositories.ParticipationRepository;
using dotNet_backend.Repositories.RequestRepository;
using dotNet_backend.Repositories.UserRepository;
using dotNet_backend.Services.AthleteService;
using dotNet_backend.Services.AuthService;
using dotNet_backend.Services.ClubService;
using dotNet_backend.Services.CoachService;
using dotNet_backend.Services.CompetitionService;
using dotNet_backend.Services.ParticipationService;
using dotNet_backend.Services.RankService;
using dotNet_backend.Services.RegisterService;
using dotNet_backend.Services.RequestService;
using dotNet_backend.Services.SMTP;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace dotNet_backend.Helpers.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IClubRepository, ClubRepository>();
            services.AddTransient<ICoachRepository, CoachRepository>();
            services.AddTransient<IRequestRepository, RequestRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IAthleteRepository, AthleteRepository>();
            services.AddTransient<IParticipationRepository, ParticipationRepository>();
            services.AddTransient<ICompetitionRepository, CompetitionRepository>();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ISMTPService, SMTPService>();
            services.AddTransient<IRegisterService, RegisterService>();
            services.AddTransient<IClubService, ClubService>();
            services.AddTransient<ICoachService, CoachService>();
            services.AddTransient<IRequestService, RequestService>();
            services.AddTransient<IAthleteService, AthleteService>();
            services.AddTransient<IParticipationService, ParticipationService>();
            services.AddTransient<ICompetitionService, CompetitionService>();
            services.AddTransient<IRankService, RankService>();
            return services;
        }
        

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Karate API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                option.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            return services;
        }

        public static IServiceCollection AddAuthentications(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidAudience = configuration["JWT:ValidAudience"],
                        ValidIssuer = configuration["JWT:ValidIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
                    };
                });
            return services;
        }
    }
}