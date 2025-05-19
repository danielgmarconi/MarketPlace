using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Domain.Entities
{
    public sealed class User : Entity
    {
        public string? FullName { get; private set; }
        public string? Email { get; private set; }
        public string? Password { get; private set; }
        public string? Status { get; private set; }
        public bool? IsBlocked { get; private set; }
    }
}
