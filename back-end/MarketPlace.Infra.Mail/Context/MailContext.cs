using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Infra.Mail.Context
{
    public class MailContext
    {
        public string host { get; set; }
        public int Port { get; set; }
        public string EmailFrom { get; set; }
        public string Password { get; set; }
    }
}
