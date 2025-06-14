using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Domain.Entities
{
    public sealed class Bio : Entity
    {
        public int? UserId { get; set; }
        public string? StoreName { get; set; }
        public string? Description { get; set; }
    }
}
