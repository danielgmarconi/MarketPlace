using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Domain.Interfaces
{
    public interface IMessageLocalizer
    {
        string this[string key] { get; }
        string Get(string key, params object[] args);
    }
}
