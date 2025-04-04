using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Domain.Entities
{
    public sealed class Bio : Entity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public string FullName { get; set; }
        public string? ProfilePhotoBase64 { get; set; }
        public string? SexPerson { get; set; }
        public string? Description { get; set; }

    }
}

