using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.Domain.Entities;

namespace MarketPlace.Application.DTOs
{
    public sealed class UserDTO
    {
        public int? Id { get; set; }
        public string? UserGuid { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password {  get; set; }
        public string? Status { get; set; }
        public bool? IsBlocked { get; set; }
        public static implicit operator UserDTO(User user) => new UserDTO
        {
            Id = user.Id,
            UserGuid = user.UserGuid,
            FullName = user.FullName,
            Email = user.Email,
            Password = user.Password,
            Status = user.Status,
            IsBlocked = user.IsBlocked
        };

        public static implicit operator User(UserDTO dto) => new User
        {
            Id = dto.Id,
            UserGuid= dto.UserGuid,
            FullName = dto.FullName,
            Email = dto.Email,
            Password = dto.Password,
            Status = dto.Status,
            IsBlocked = dto.IsBlocked
        };
    }
}
