namespace dotNet_backend.Services.SMTP
{
    public interface ISMTPService
    { 
        Task SendEmailAsync(string email, string subject, string message);
    }
}
