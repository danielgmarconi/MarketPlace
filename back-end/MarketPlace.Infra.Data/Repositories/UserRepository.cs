using System.Reflection;
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
        public async Task<User> Create(User model)
        { 
            _sqlServerAdapter.Open();
            var id = await _sqlServerAdapter.ExecuteScalarAsync("spUsersCreate", model);
            return await Get(int.Parse(id.ToString()));
        }
        public async Task Remove(User model)
        {
            _sqlServerAdapter.Open();
            await _sqlServerAdapter.ExecuteNonQueryAsync("spUsersSelect", model);
        }
        public async Task<User> Get(int id)
        {
            var result = await Get(new User() { Id = id });
            return result.FirstOrDefault();
        }
        public async Task<User> Get(string email)
        {
            var result = await Get(new User() { Email = email });
            return result.FirstOrDefault();

        }
        public async Task<List<User>> Get(User model)
        {
            _sqlServerAdapter.Open();
            return await _sqlServerAdapter.ExecuteReaderAsync<User>("spUsersSelect", model);        
        }
        public async Task Update(User model)
        {
            _sqlServerAdapter.Open();
            await _sqlServerAdapter.ExecuteNonQueryAsync("spUsersUpdate", model);
        }
    }
}
