using System.Reflection;
using MarketPlace.Domain.Interfaces;
using Microsoft.Extensions.Localization;

namespace MarketPlace.Infra.Localizer
{
    public sealed class Resource { }
    public class MessageLocalizer : IMessageLocalizer
    {
        private readonly IStringLocalizer _inner;
        public MessageLocalizer(IStringLocalizerFactory factory)
        {
            _inner = factory.Create("Resource", "MarketPlace.API"); ;
        }
        public string this[string key] => _inner[key];
        public string Get(string key, params object[] args) => _inner[key, args];
    }
}
