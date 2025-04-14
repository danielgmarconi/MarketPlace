using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.Application.Common;
using MarketPlace.Application.DTOs;

namespace MarketPlace.Application.Interfaces
{
    public interface IUserService
    {
        Task<MethodResponse> GetUsers();
        Task<UserDTO> GetById(int? id);
        Task Create(UserDTO userDto);
        Task Update(UserDTO userDto);
        Task Remove(int? id);
    }
}
