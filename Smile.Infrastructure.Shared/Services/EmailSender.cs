using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;
using System.Threading.Tasks;
using Smile.Core.Application.Models.Email;
using Smile.Core.Application.Services;
using Smile.Core.Application.Settings;

namespace Smile.Infrastructure.Shared.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly SendGridClient emailClient;
        private readonly EmailSettings emailSettings;

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            this.emailSettings = emailSettings.Value;

            this.emailClient = new SendGridClient(this.emailSettings.ApiKey);
        }

        public async Task<bool> Send(EmailMessage emailMessage)
        {
            var emailContent = new EmailContent(!string.IsNullOrEmpty(emailMessage.SenderEmail) ? emailMessage.SenderEmail : emailSettings.Sender, emailMessage.Email);

            var email = MailHelper.CreateSingleEmail(emailContent.FromAddress, emailContent.ToAddress, emailMessage.Subject, emailMessage.Message, emailMessage.Message);

            var response = await emailClient.SendEmailAsync(email);

            return response.StatusCode == HttpStatusCode.Accepted;
        }
    }
}