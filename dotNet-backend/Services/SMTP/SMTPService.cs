using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace dotNet_backend.Services.SMTP
{
    public class SMTPService : ISMTPService
    {
        private readonly IConfiguration configuration;
        private readonly ILogger logger;

        public SMTPService(IConfiguration configuration, ILogger<SMTPService> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            string sendGridApiKey = configuration["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"];
            if (string.IsNullOrEmpty(sendGridApiKey))
            {
                throw new Exception("The 'SendGridApiKey' is not configured");
            }

            var client = new SendGridClient(sendGridApiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("[ghercadarius@gmail.com]", "[karate_test]"),
                Subject = subject,
                PlainTextContent = htmlMessage,
                HtmlContent = htmlMessage
            };
            msg.AddTo(new EmailAddress(email));

            var response = await client.SendEmailAsync(msg);
            if (response.IsSuccessStatusCode)
            {
                logger.LogInformation("Email queued successfully");
            }
            else
            {
                logger.LogError("Failed to send email");
                // Adding more information related to the failed email could be helpful in debugging failure,
                // but be careful about logging PII, as it increases the chance of leaking PII
            }

        }
    }
}
