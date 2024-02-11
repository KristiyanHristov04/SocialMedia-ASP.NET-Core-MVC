using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;
using SendGrid;
using SocialMedia.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SocialMedia.Services
{
    public class CustomEmailSender : ICustomEmailSender
    {
        private ILogger logger;
        public CustomEmailSender(
            ILogger<CustomEmailSender> logger,
            IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            this.logger = logger;
            Options = optionsAccessor.Value;
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
            var from_email = new EmailAddress(email); // Must be a verified sender
            //from sendgrid. That's why I need to use my email address otherwise
            //it does not work.
            var to_email = new EmailAddress("kristiyan_hristov04@abv.bg");
            var plainTextContent = $"{email}: {message}";
            var msg = MailHelper.CreateSingleEmail(from_email, to_email, subject, plainTextContent, null);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);

            return response.IsSuccessStatusCode;
        }
    }
}
