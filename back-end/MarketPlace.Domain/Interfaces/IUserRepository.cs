using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.Domain.Entities;

namespace MarketPlace.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> Get(int id);
        Task<List<User>> Get(string email);
        Task<int> Create(User model);
        Task Update(User model);
        Task Remove(User model);
    }
}
