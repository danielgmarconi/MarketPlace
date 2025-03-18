using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MarketPlace.Application.DTOs;
using MarketPlace.Application.Interfaces;
using MarketPlace.Domain.Entities;
using MarketPlace.Domain.Interfaces;

namespace MarketPlace.Application.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        public UserService(IUserRepository userRepository, IMapper mapper, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            var usersEntity = await _userRepository.GetUsers();
            var x = _jwtService.GenerateToken(1, "a@a.com");
            return _mapper.Map<IEnumerable<UserDTO>>(usersEntity);
        }

        public async Task<UserDTO> GetById(int? id)
        {
            var userEntity = await _userRepository.GetById(id);
            return _mapper.Map<UserDTO>(userEntity);
        }

        public async Task Create(UserDTO userDto)
        {
            var userEntity = _mapper.Map<User>(userDto);
            await _userRepository.Create(userEntity);
        }

        public async Task Update(UserDTO userDto)
        {
            var userEntity = _mapper.Map<User>(userDto);
            await _userRepository.Update(userEntity);
        }

        public async Task Remove(int? id)
        {
            var userEntity = _userRepository.GetById(id).Result;
            await _userRepository.Remove(userEntity);
        }
    }
}
