using System.Net.Mail;
using System.Net;
using API.Configuration;

namespace API.Services
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
    public class EmailSenderService : IEmailSenderService
    {
        private readonly EmailCredential _emailCredential;
        public EmailSenderService(EmailCredential emailCredential)
        {
            _emailCredential = emailCredential;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = _emailCredential.Email;
            var pw = _emailCredential.Password;

            var client = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, pw)
            };

            var msg = new MailMessage(from: mail, to: email, subject, message);
            msg.IsBodyHtml = true;
            await client.SendMailAsync(msg);
        }
    }
}
