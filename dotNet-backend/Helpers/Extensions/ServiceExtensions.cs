using dotNet_backend.Repositories.UserRepository;
using dotNet_backend.Services.AuthService;
using dotNet_backend.Services.SMTP;

namespace dotNet_backend.Helpers.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ISMTPService, SMTPService>();
            return services;
        }
    }
}
