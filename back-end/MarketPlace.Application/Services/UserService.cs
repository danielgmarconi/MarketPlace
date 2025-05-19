using System.Text.RegularExpressions;
using AutoMapper;
using MarketPlace.Application.Common;
using MarketPlace.Application.DTOs;
using MarketPlace.Application.Interfaces;
using MarketPlace.Domain.Entities;
using MarketPlace.Domain.Interfaces;

namespace MarketPlace.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        { 
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public Task<MethodResponse> Create(UserDTO model)
        {
            throw new NotImplementedException();
        }

        public async Task<MethodResponse> Get(int id)
        {
            var result = new MethodResponse();
            try
            {
                result.Response = _mapper.Map<List<UserDTO>>(await _userRepository.Get(new User() { Id = id }));
                result.Success = true;
                result.StatusCode = 200;
            }
            catch (Exception e)
            {
                result.StatusCode = 500;
                result.Message = e.Message;
            }
            return result;
        }

        public Task<MethodResponse> Get(string email)
        {
            throw new NotImplementedException();
        }

        public Task<MethodResponse> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task<MethodResponse> Update(UserDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
