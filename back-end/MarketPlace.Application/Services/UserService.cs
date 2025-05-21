using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using AutoMapper;
using FluentValidation;
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
        private readonly IValidator<UserDTO> _validator;
        public UserService(IUserRepository userRepository, IMapper mapper, IValidator<UserDTO> validator)
        { 
            _userRepository = userRepository;
            _mapper = mapper;
            _validator = validator;
        }
        public Task<MethodResponse> Create(UserDTO model)
        {
            throw new NotImplementedException();
        }

        public async Task<MethodResponse> Get(int id)
        {
            var x = await _validator.ValidateAsync(new UserDTO());
            var x2 = x.IsValid;
            var result = new MethodResponse();
            try
            {
                result.Response = _mapper.Map<List<UserDTO>>(await _userRepository.Get(new User(id)));
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
