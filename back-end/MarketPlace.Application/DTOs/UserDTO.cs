using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.DTOs
{
    public sealed class UserDTO
    {
        public int? Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password {  get; set; }
        public string? Status { get; set; }
        public bool? IsBlocked { get; set; }
    }
}
