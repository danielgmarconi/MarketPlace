using MarketPlace.Application.DTOs;
using MarketPlace.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorizationController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthorizationController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpPost]
    [Route("Authentication")]
    [AllowAnonymous]
    public async Task<IActionResult> Authentication([FromBody] AuthenticationDTO authenticationDTO)
    {
        var result = await _userService.Authentication(authenticationDTO);
        return StatusCode(result.StatusCode, result);
    }
    [HttpPost]
    [Route("Register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
    {
        var result = await _userService.Create(userDTO);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("EmailExists/{email}")]
    [AllowAnonymous]
    public async Task<IActionResult> EmailExists(string email)
    {
        var result = await _userService.EmailExists(email);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("ActivateAccount/{guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> ActivateAccount(string guid)
    {
        var result = await _userService.ActivateAccount(guid);
        return StatusCode(result.StatusCode, result);
    }
    [HttpGet("LostPassword/{email}")]
    [AllowAnonymous]
    public async Task<IActionResult> LostPassword(string email)
    {
        var result = await _userService.LostPassword(email);
        return StatusCode(result.StatusCode, result);
    }
    [HttpPost]
    [Route("ChangePassword")]
    [AllowAnonymous]
    public async Task<IActionResult> RegistChangePassworder([FromBody] UserDTO userDTO)
    {
        var result = await _userService.ChangePassword(userDTO);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet]
    [Route("teste")]
    [AllowAnonymous]
    public async Task<IActionResult> teste()
    {

        return Ok(await _userService.teste() );

    }
}
