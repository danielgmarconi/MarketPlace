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
        Task<User> Get(int id);
        Task<User> Get(string email);
        Task<List<User>> Get(User model);
        Task<User> Create(User model);
        Task Update(User model);
        Task Remove(User model);
    }
}
