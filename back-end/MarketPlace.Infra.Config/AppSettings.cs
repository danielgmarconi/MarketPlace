using System.Security.Principal;
using MarketPlace.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace MarketPlace.Infra.Config
{
    public class AppSettings : IAppSettings
    {
        private readonly IConfiguration _configuration;
        public AppSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string FrontEndUrl()
        {
            return _configuration["FrontEndUrl"] ?? string.Empty;
        }
        public string AccountActivation(string guid)
        {
           return FrontEndUrl() + "activation/" + guid;
        }
    }
}
