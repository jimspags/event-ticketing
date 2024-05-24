using System.Net.Mail;
using System.Net;

namespace API.Services
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
    public class EmailSenderService : IEmailSenderService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            //var mail = Environment.GetEnvironmentVariable("");
            //var pw = Environment.GetEnvironmentVariable("");
            
            var mail = "jimspuldev@outlook.com";
            var pw = "Stephencurry30";

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
