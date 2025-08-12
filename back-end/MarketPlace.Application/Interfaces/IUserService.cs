using MarketPlace.Application.Common;
using MarketPlace.Application.DTOs;

namespace MarketPlace.Application.Interfaces;

public interface IUserService
{
    Task<MethodResponse> Get(int id);
    Task<MethodResponse> Get(UserDTO model);
    Task<MethodResponse> EmailExists(string email);
    Task<MethodResponse> Create(UserDTO model);
    Task<MethodResponse> Update(UserDTO model);
    Task<MethodResponse> Remove(int id);
    Task<MethodResponse> Authentication(AuthenticationDTO authenticationDto);
    Task<MethodResponse> ActivateAccount(string guid);
    Task<MethodResponse> LostPassword(string email);
    Task<MethodResponse> ChangePassword(UserDTO model);
}
