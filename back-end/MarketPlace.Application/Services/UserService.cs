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
        private readonly IValidator<UserDTO> _validator;
        private readonly IValidator<AuthenticationDTO> _validatorAuthentication;
        public readonly IEncryptionService _encryptionService;
        private readonly IJwtService _jwtService;
        public UserService(IUserRepository userRepository, 
                           IValidator<UserDTO> validator, 
                           IValidator<AuthenticationDTO> validatorAuthentication, 
                           IEncryptionService encryptionService,
                           IJwtService jwtService)
        { 
            _userRepository = userRepository;
            _validator = validator;
            _validatorAuthentication = validatorAuthentication;
            _encryptionService = encryptionService;
            _jwtService = jwtService;
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
                model.UserGuid = Guid.NewGuid().ToString();
                model.Password =  _encryptionService.Encrypt(model.Password);
                model = await _userRepository.Create(model);
                model.Password = string.Empty;
                result.Update(true, 201, "Created successfully", model);
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
                var user = await _userRepository.Get(id);
                if(user != null)
                    user.Password = null;
                result.Update(true, 200, "Successfully executed", user == null ? null : (UserDTO)user);
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
                var list = (await _userRepository.Get(model)).Select(x => (UserDTO)x).ToList();
                foreach (var user in list)
                    user.Password = null;
                result.Update(true, 200, "Successfully executed", list);
            }
            catch (Exception e)
            {
                result.Update(500, "Error", e.Message);
            }
            return result;
        }
        public async Task<MethodResponse> EmailExists(string email)
        {
            var result = new MethodResponse();
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    result.Update(400, "Bad Request");
                    return result;
                }
                var list = await _userRepository.Get(new User() { Email = email });

                result.Update(true, 200, "Successfully executed", list.Count > 0);
            }
            catch (Exception e)
            {
                result.Update(500, "Error", e.Message);
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
                await _userRepository.Remove(id);
                result.Update(true, 201, "Created successfully");
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

                result.Update(true, 200, "Created successfully", (UserDTO)entity);
            }
            catch (Exception e)
            {
                result.Update(500, "Error", e.Message);
            }
            return result;
        }
        public async Task<MethodResponse> Authentication(AuthenticationDTO model)
        {
            var result = new MethodResponse();
            try
            {
                if (model == null)
                {
                    result.Update(400, "Invalid data");
                    return result;
                }
                var validatorResult = await _validatorAuthentication.ValidateAsync(model);
                if (!validatorResult.IsValid)
                {
                    result.Update(500, "Invalid data", validatorResult.Errors.Select(e => e.ErrorMessage).ToList());
                    return result;
                }
                var user = await _userRepository.Get(model.Email);
                if (user == null)
                {
                    result.Update(500, "Error", "Account not registered");
                    return result;
                }
                if (user.Status.Equals("P"))
                {
                    user.Password = string.Empty;
                    result.Update(false, 500, "Account not activated", (UserDTO)user);
                    return result;
                }
                if (!_encryptionService.Valid(user.Password, model.Password))
                    result.Update(401, "Error", "Unauthorized");
                else
                    result.Update(true, 200, "Successfully executed", _jwtService.GenerateToken(user.Id.Value, user.Email));
            }
            catch (Exception e)
            {
                result.Update(500, "Error", e.Message);
            }
            return result;
        }
    }
}
