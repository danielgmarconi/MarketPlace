using MarketPlace.Infra.Mail.Context;
using Microsoft.Extensions.Configuration;
namespace MarketPlace.Infra.Mail.Service
{
    public class MailService
    {
        private readonly MailContext _mailContext;
        public MailService(IConfiguration configuration)
        {
            var mailContext = configuration.GetSection("Smtp");

            if (!mailContext.Exists())
                throw new Exception("Jwt config section not found.");
            _mailContext = mailContext.Get<MailContext>();
        }
    }
}
