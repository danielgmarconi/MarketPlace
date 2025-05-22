using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
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
        public async Task<MethodResponse> Create(UserDTO model)
        {
            var result = new MethodResponse();
            if (model == null)
            {
                result.StatusCode = 400;
                result.Message = "Bad Request";
                return result;
            }
            try
            {
                var validatorResult = await _validator.ValidateAsync(model);
                if (!validatorResult.IsValid)
                {
                    result.StatusCode = 500;
                    result.Message = "Invalid data";
                    result.Response = validatorResult.Errors.Select(e => e.ErrorMessage).ToList(); ;
                }
                var entity = _mapper.Map<User>(model);

                //entity.PasswordUpdate(_encryptionService.Encrypt(userEntity.Password));
                //userEntity.DateCreated = DateTime.Now;
                //await _userRepository.Create(userEntity);
                //userEntity.PasswordUpdate("");
                //result.Success = true;
                //result.StatusCode = 201;
                //result.Response = _mapper.Map<UserDTO>(userEntity);
            }
            catch (Exception e)
            {
                result.StatusCode = 500;
                result.Message = e.Message;
            }
            return result;
        }

        public async Task<MethodResponse> Get(int id)
        {
            var result = new MethodResponse();
            try
            {
                result.Response = _mapper.Map<List<UserDTO>>(await _userRepository.Get(id));
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
