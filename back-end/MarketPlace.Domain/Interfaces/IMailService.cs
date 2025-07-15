using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Domain.Interfaces
{
    public interface IMailService
    {
        Task Send(string mailTo, string subject, string body);
    }
}
