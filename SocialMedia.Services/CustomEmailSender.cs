using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using SocialMedia.Services.Interfaces;

namespace SocialMedia.Services
{
    public class CustomEmailSender : ICustomEmailSender
    {
        private ILogger logger;
        private IConfiguration configuration;
        private string email;
        public CustomEmailSender(
            ILogger<CustomEmailSender> logger,
            IOptions<AuthMessageSenderOptions> optionsAccessor,
            IConfiguration configuration)
        {
            this.logger = logger;
            Options = optionsAccessor.Value;
            this.configuration = configuration;
            email = this.configuration["Email"]!;
        }

        public AuthMessageSenderOptions Options { get; } //Set with Secret Manager.

        public async Task<bool> SendEmailAsync(string email, string subject, string message)
        {
            if (string.IsNullOrEmpty(Options.SendGridKey))
            {
                throw new Exception("Null SendGridKey");
            }

            var apiKey = Options.SendGridKey;
            var client = new SendGridClient(apiKey);
            var from_email = new EmailAddress(this.email, email); // Must be a verified sender
            //from sendgrid. That's why I need to use my email address otherwise
            //it does not work.
            var to_email = new EmailAddress(this.email);
            var plainTextContent = $"{email}: {message}";
            var msg = MailHelper.CreateSingleEmail(from_email, to_email, subject, plainTextContent, null);
            EmailAddress replyTo = new EmailAddress(email);
            msg.SetReplyTo(replyTo);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);

            return response.IsSuccessStatusCode;
        }
    }
}
