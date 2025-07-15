using System.Net;
using System.Net.Http;
using System.Net.Mail;
using MarketPlace.Domain.Interfaces;
using MarketPlace.Infra.Mail.Context;
using Microsoft.Extensions.Configuration;
namespace MarketPlace.Infra.Mail.Service
{
    public class MailService : IMailService
    {
        private readonly MailContext _mailContext;
        public MailService(IConfiguration configuration)
        {
            var mailContext = configuration.GetSection("Smtp");

            if (!mailContext.Exists())
                throw new Exception("Jwt config section not found.");
            _mailContext = mailContext.Get<MailContext>();
        }
        public async Task Send(string mailTo, string subject, string body)
        {
            using (var smtp = new SmtpClient(_mailContext.host, _mailContext.Port) { EnableSsl = true, 
                                                                                     UseDefaultCredentials = false, 
                                                                                     Credentials = new NetworkCredential(_mailContext.EmailFrom, _mailContext.Password)})
            {
                var mailMessage = new MailMessage
                {
                    IsBodyHtml = true,
                    From = new MailAddress(_mailContext.EmailFrom),
                    Subject = subject,
                    Body = body
                };
                mailMessage.To.Add(mailTo);
                await smtp.SendMailAsync(mailMessage);

            }

        }
    }

}
