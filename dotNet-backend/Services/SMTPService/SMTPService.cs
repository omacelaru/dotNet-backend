using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace dotNet_backend.Services.SMTP
{
    public class SMTPService : ISMTPService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<SMTPService> _logger;

        public SMTPService(IConfiguration configuration, ILogger<SMTPService> logger)
        {
            this._configuration = configuration;
            this._logger = logger;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            _logger.LogInformation("Sending email to {email} with subject {subject}", email, subject);
            string sendGridApiKey = _configuration["SendGridApiKey"];
            if (string.IsNullOrEmpty(sendGridApiKey))
            {
                _logger.LogError("The 'SendGridApiKey' is not configured");
                throw new Exception("The 'SendGridApiKey' is not configured");
            }

            var client = new SendGridClient(sendGridApiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("ghercadarius@gmail.com", "karate_test"),
                Subject = subject,
                PlainTextContent = htmlMessage,
                HtmlContent = htmlMessage
            };
            msg.AddTo(new EmailAddress(email));
            var response = await client.SendEmailAsync(msg);
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Email queued successfully");
            }
            else
            {
                _logger.LogError("Failed to send email");
                // Adding more information related to the failed email could be helpful in debugging failure,
                // but be careful about logging PII, as it increases the chance of leaking PII
            }

        }
    }
}
