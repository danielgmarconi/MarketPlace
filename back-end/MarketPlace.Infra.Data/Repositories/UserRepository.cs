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
        public async Task<int> Create(User model)
        { 
            _sqlServerAdapter.Open();
            var result = await _sqlServerAdapter.ExecuteScalarAsync("spUsersCreate", model);
            return int.Parse(result.ToString() ?? "0");  
        }
        public async Task Remove(User model)
        {
            _sqlServerAdapter.Open();
            await _sqlServerAdapter.ExecuteNonQueryAsync("spUsersSelect", model);
        }
        public async Task<List<User>> Get(int id)
        {
            _sqlServerAdapter.Open();

            return await _sqlServerAdapter.ExecuteReaderAsync<User>("spUsersSelect", new User(id)); ;               
        }
        public async Task<List<User>> Get(string email)
        {
            _sqlServerAdapter.Open();

            return await _sqlServerAdapter.ExecuteReaderAsync<User>("spUsersSelect", new User(email)); ;
        }
        public async Task Update(User model)
        {
            _sqlServerAdapter.Open();
            await _sqlServerAdapter.ExecuteNonQueryAsync("spUsersUpdate", model);
        }
    }
}
