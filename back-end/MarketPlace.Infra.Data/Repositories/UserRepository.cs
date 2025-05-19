using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.SqlServerAdapter;
using MarketPlace.Domain.Entities;
using MarketPlace.Domain.Interfaces;

namespace MarketPlace.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ISQLServerAdapter _sqlServerAdapter;
        public UserRepository(ISQLServerAdapter sqlServerAdapter)
        {
            _sqlServerAdapter = sqlServerAdapter;
        }

        public async Task<int> Create(User model)
        { 
            _sqlServerAdapter.Open();
            var result = await _sqlServerAdapter.ExecuteReaderAsync<User>("spUsersSelect", model);
            return int.Parse(result.ToString());  
        }

        public async Task Remove(User model)
        {
            _sqlServerAdapter.Open();
            _sqlServerAdapter.ExecuteReaderAsync<User>("spUsersSelect", model);
        }

        public async Task<List<User>> Get(User model)
        {
            _sqlServerAdapter.Open();
            return await _sqlServerAdapter.ExecuteReaderAsync<User>("spUsersSelect", model);              
        }

        public async Task Update(User model)
        {
            _sqlServerAdapter.Open();
            _sqlServerAdapter.ExecuteReaderAsync<User>("spUsersSelect", model);
        }
    }
}
