using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        private readonly IEncryptionService _encryptionService;
        public UserService(IUserRepository userRepository, IMapper mapper, IJwtService jwtService, IEncryptionService encryptionService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtService = jwtService;
            _encryptionService = encryptionService;
        }

        //public async Task<MethodResponse> GetUsers()
        //{
        //    var methodResponse = new MethodResponse();
        //    var usersEntity = await _userRepository.GetUsers();
        //    methodResponse.Success = true;
        //    methodResponse.Response = _mapper.Map<IEnumerable<UserDTO>>(usersEntity);
        //    if (usersEntity == null || usersEntity.Count() == 0)
        //    {
        //        methodResponse.StatusCode = 400;
        //        methodResponse.Message = "Users not found";
        //    }




        //    return methodResponse;
        //}
        //public async Task<IEnumerable<UserDTO>> GetUsers()
        //{
        //    var usersEntity = await _userRepository.GetUsers();
        //    return _mapper.Map<IEnumerable<UserDTO>>(usersEntity);
        //}
        public async Task<UserDTO> GetById(int? id)
        {
            var userEntity = await _userRepository.GetById(id);
            return _mapper.Map<UserDTO>(userEntity);
        }

        public async Task Create(UserDTO userDto)
        {
            var userEntity = _mapper.Map<User>(userDto);

            userEntity.PasswordUpdate(_encryptionService.Encrypt(userEntity.Password));
            //var x = _jwtService.GenerateToken(1, "a@a.com");
            //var xxx = _encryptionService.Encrypt("teste");
            //var x1 = _encryptionService.Valid(xxx, "teste1");
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
