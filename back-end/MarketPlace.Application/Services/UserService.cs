using System.Reflection;
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
        public readonly IEncryptionService _encryptionService;
        public UserService(IUserRepository userRepository, IMapper mapper, IValidator<UserDTO> validator, IEncryptionService encryptionService)
        { 
            _userRepository = userRepository;
            _mapper = mapper;
            _validator = validator;
            _encryptionService = encryptionService;
        }
        public async Task<MethodResponse> Create(UserDTO model)
        {
            var result = new MethodResponse();
            if (model == null)
            {
                result.Update(400, "Bad Request");
                return result;
            }
            try
            {
                var validatorResult = await _validator.ValidateAsync(model, options => options.IncludeRuleSets("Create"));
                if (!validatorResult.IsValid)
                {
                    result.Update(500, "Invalid data", validatorResult.Errors.Select(e => e.ErrorMessage).ToList());
                    return result;
                }
                var entity = _mapper.Map<User>(model);
                entity.Password =  _encryptionService.Encrypt(entity.Password);
                entity = await _userRepository.Create(entity);
                entity.Password = string.Empty;
                result.Update(true, 201, "Created successfully", _mapper.Map<UserDTO>(entity));
            }
            catch (Exception e)
            {
                result.Update(500, "Error", e.Message);
            }
            return result;
        }

        public async Task<MethodResponse> Get(int id)
        {
            var result = new MethodResponse();
            try
            {
                if (id <= 0)
                {
                    result.Update(400, "Bad Request");
                    return result;
                }
                result.Update(true, 200, "Successfully executed", _mapper.Map<UserDTO>(await _userRepository.Get(id)));
            }
            catch (Exception e)
            {
                result.Update(500, "Error", e.Message);
            }
            return result;
        }

        public async Task<MethodResponse> Get(UserDTO model)
        {
            var result = new MethodResponse();
            try
            {
                if (model == null)
                {
                    result.Update(400, "Bad Request");
                    return result;
                }
                result.Update(true, 200, "Successfully executed", _mapper.Map<List<UserDTO>>(await _userRepository.Get(_mapper.Map<User>(model))));
            }
            catch (Exception e)
            {
                result.StatusCode = 500;
                result.Message = e.Message;
            }
            return result;
        }

        public async Task<MethodResponse> Remove(int id)
        {
            var result = new MethodResponse();
            if (id <= 0)
            {
                result.Update(400, "Bad Request");
                return result;
            }
            try
            {
                //var validatorResult = await _validator.ValidateAsync(model);
                //if (!validatorResult.IsValid)
                //{
                //    result.Update(500, "Invalid data", validatorResult.Errors.Select(e => e.ErrorMessage).ToList());
                //    return result;
                //}
                //var entity = _mapper.Map<User>(model);
                //entity.Password = _encryptionService.Encrypt(entity.Password);
                //entity = await _userRepository.Create(entity);
                //entity.Password = string.Empty;
                //result.Update(true, 201, "Created successfully", _mapper.Map<UserDTO>(entity));
            }
            catch (Exception e)
            {
                result.Update(500, "Error", e.Message);
            }
            return result;
        }

        public async Task<MethodResponse> Update(UserDTO model)
        {
            var result = new MethodResponse();
            if (model == null)
            {
                result.Update(400, "Bad Request");
                return result;
            }
            try
            {
                var validatorResult = await _validator.ValidateAsync(model, options => options.IncludeRuleSets("Update"));
                if (!validatorResult.IsValid)
                {
                    result.Update(500, "Invalid data", validatorResult.Errors.Select(e => e.ErrorMessage).ToList());
                    return result;
                }
                var entity = await _userRepository.Get(model.Id.Value);
                entity.Update(model.FullName ?? entity.FullName,
                              model.Email,
                              model.Password == null ? entity.Password : _encryptionService.Encrypt(model.Password));
                await _userRepository.Update(entity);
                result.Update(true, 200, "Created successfully", _mapper.Map<UserDTO>(entity));
            }
            catch (Exception e)
            {
                result.Update(500, "Error", e.Message);
            }
            return result;
        }
    }
}
