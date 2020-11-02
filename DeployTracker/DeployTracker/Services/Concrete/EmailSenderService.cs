using DeployTracker.Options;
using DeployTracker.Services.Contracts;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading.Tasks;

namespace DeployTracker.Services.Concrete
{
    public class EmailSenderService: IEmailSenderService
    {
        private readonly EmailServiceOptions _options;

        public EmailSenderService(IOptions<EmailServiceOptions> options)
        {
            _options = options.Value;
        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта",_options.MailFrom));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_options.Host, _options.Port, _options.UseSSL);
                await client.AuthenticateAsync(_options.MailFrom, _options.MailFromPassword);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
